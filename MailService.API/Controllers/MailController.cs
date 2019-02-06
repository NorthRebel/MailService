using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MailService.API.Infrastructure;
using MailService.API.Models;
using MailService.Domain.Entities;
using MailService.Domain.Repositories;
using MailService.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace MailService.API.Controllers
{
    using CorrespondenceResult = Domain.Entities.CorrespondenceResult;

    [ApiController]
    [Route("api/mails")]
    public class MailController : Controller
    {
        /// <inheritdoc cref="ICorrespondenceRepository"/>
        private readonly ICorrespondenceRepository _correspondenceRepository;

        /// <inheritdoc cref="IRecipientRepository"/>
        private readonly IRecipientRepository _recipientRepository;

        /// <inheritdoc cref="IMessageRepository"/>
        private readonly IMessageRepository _messageRepository;

        /// <inheritdoc cref="IEmailService"/>
        private readonly IEmailService _emailService;

        /// <summary>
        /// 
        /// </summary>
        private readonly IEmailSender _emailSender;

        public MailController(ICorrespondenceRepository correspondenceRepository,
            IRecipientRepository recipientRepository,
            IMessageRepository messageRepository,
            IEmailService emailService,
            IEmailSender emailSender)
        {
            _correspondenceRepository = correspondenceRepository;
            _recipientRepository = recipientRepository;
            _messageRepository = messageRepository;
            _emailService = emailService;
            _emailSender = emailSender;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Send([FromBody] MessageToSend message)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            int messageId = await CreateNewMessage(message.Subject, message.Body);

            IEnumerable<Recipient> recipients = await SaveRecipients(message.Recipients);

            foreach (Recipient recipient in recipients)
            {
                Correspondence correspondence =
                    await SendMessage(messageId,
                        recipient.Id,
                        new EmailAddress(_emailSender.Address) { Name = _emailSender.Name },
                        new EmailAddress(recipient.Email),
                        message.Subject,
                        message.Body);

                _correspondenceRepository.Add(correspondence);
            }

            await _correspondenceRepository.UnitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Show), new { messageId }, "Hello!");
        }

        private async Task<int> CreateNewMessage(string subject, string body)
        {
            var newMsg = new Message
            {
                CreateDate = DateTime.Now,
                Subject = subject,
                Body = body
            };
            _messageRepository.Add(newMsg);
            await _messageRepository.UnitOfWork.SaveChangesAsync();

            return newMsg.Id;
        }

        private async Task<IEnumerable<Recipient>> SaveRecipients(IEnumerable<string> recipientMails)
        {
            var recipients = new List<Recipient>();
            bool isNewAdded = false;
            foreach (string recipientMail in recipientMails)
            {
                Recipient recipient = await _recipientRepository.GetByEmail(recipientMail);
                if (recipient == null)
                {
                    recipient = _recipientRepository.Add(new Recipient { Email = recipientMail });
                    isNewAdded = true;
                }

                recipients.Add(recipient);
            }

            if (isNewAdded)
                await _recipientRepository.UnitOfWork.SaveChangesAsync();

            return recipients;
        }

        private async Task<Correspondence> SendMessage(int messageId, int recipientId, EmailAddress sender,
            EmailAddress recipient, string subject, string body)
        {
            var correspondence = new Correspondence { MessageId = messageId, RecipientId = recipientId };

            try
            {
                correspondence.SendDate = DateTime.Now;

                await _emailService.SendAsync(new EmailMessage(sender, recipient, subject, body));

                correspondence.Result = CorrespondenceResult.Ok;
            }
            catch (Exception ex)
            {
                correspondence.Result = CorrespondenceResult.Failed;
                correspondence.ErrorMessage = ex.Message;
            }

            return correspondence;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SentMessage>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Show()
        {
            IEnumerable<Message> messages = await _messageRepository.GetAll();

            var result = new List<SentMessage>();

            foreach (Message message in messages)
            {
                var sentMessage = new SentMessage
                {
                    Id = message.Id,
                    Subject = message.Subject,
                    Body = message.Body,
                    CreateDate = message.CreateDate
                };

                sentMessage.Correspondences = await GetCorrespondenceOfMessage(message.Id);
                
                result.Add(sentMessage);
            }
            
            if (result.Any())
                return Ok(result);

            return NoContent();
        }

        [HttpGet("{messageId:int}")]
        [ProducesResponseType(typeof(SentMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Show(int messageId)
        {
            Message message = await _messageRepository.GetById(messageId);

            if (message != null)
            {
                var result = new SentMessage
                {
                    Id = message.Id,
                    Subject = message.Subject,
                    Body = message.Body,
                    CreateDate = message.CreateDate
                };

                result.Correspondences = await GetCorrespondenceOfMessage(messageId);

                return Ok(result);
            }

            return NotFound();
        }

        private async Task<IEnumerable<RecipientCorrespondence>> GetCorrespondenceOfMessage(int messageId)
        {
            var result = new List<RecipientCorrespondence>();

            IEnumerable<Correspondence> correspondences = await _correspondenceRepository.GetAllByMessageId(messageId);

            foreach (Correspondence correspondence in correspondences)
            {
                Recipient recipient = await _recipientRepository.GetById(correspondence.RecipientId);
                if (recipient != null)
                {
                    var recipientCorrespondence = new RecipientCorrespondence
                    {
                        Email = recipient.Email,
                        ErrorMessage = correspondence.ErrorMessage,
                        SendDate = correspondence.SendDate
                    };

                    Enum.TryParse<Models.CorrespondenceResult>(correspondence.Result.ToString(),
                        out var correspondenceResult);
                    recipientCorrespondence.Result = correspondenceResult;

                    result.Add(recipientCorrespondence);
                }
            }

            return result;
        }
    }
}
