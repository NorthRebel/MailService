using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

        public MailController(ICorrespondenceRepository correspondenceRepository,
            IRecipientRepository recipientRepository,
            IMessageRepository messageRepository,
            IEmailService emailService)
        {
            _correspondenceRepository = correspondenceRepository;
            _recipientRepository = recipientRepository;
            _messageRepository = messageRepository;
            _emailService = emailService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Send([FromBody] MessageToSend message)
        {
            int messageId = await CreateNewMessage(message.Subject, message.Body);

            IEnumerable<Recipient> recipients = await SaveRecipients(message.Recipients);

            foreach (Recipient recipient in recipients)
            {
                Correspondence correspondence =
                    await SendMessage(messageId,
                                      recipient.Id,
                                      new EmailAddress(""),
                                      new EmailAddress(recipient.Email),
                                      message.Subject,
                                      message.Body);

                _correspondenceRepository.Add(correspondence);
            }

            await _correspondenceRepository.UnitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Show), 1);
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

        private async Task<Correspondence> SendMessage(int messageId, int recipientId, EmailAddress sender, EmailAddress recipient, string subject, string body)
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
        public async Task<IActionResult> Show()
        {
            return Ok();
        }

        [HttpGet("/{messageId:int}")]
        public async Task<IActionResult> Show(int messageId)
        {
            return Ok();
        }
    }
}
