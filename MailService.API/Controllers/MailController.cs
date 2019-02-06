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

    /// <summary>
    /// MVC controller to access REST service
    /// </summary>
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

        /// <inheritdoc cref="IEmailSender"/>
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Constructor that used to inject external dependencies
        /// </summary>
        /// <param name="correspondenceRepository">Repository for manage set of <see cref="Correspondence"/> entities</param>
        /// <param name="recipientRepository">Repository for manage set of <see cref="Recipient"/> entities</param>
        /// <param name="messageRepository">Repository for manage set of <see cref="Message"/> entities</param>
        /// <param name="emailService">Service to work with mail</param>
        /// <param name="emailSender">Credentials that used to send messages</param>
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

        /// <summary>
        /// Creates an email message and sends it to recipients.
        /// Sending result is saved to database.
        /// </summary>
        /// <param name="message">Message to send that include recipients</param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Send([FromBody] MessageToSend message)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            int messageId = await SaveMessage(message.Subject, message.Body);

            IEnumerable<Recipient> recipients = await SaveRecipients(message.Recipients);

            foreach (Recipient recipient in recipients)
            {
                Correspondence correspondence =
                    await SendMessage(
                        new EmailAddress(_emailSender.Address, _emailSender.Name),
                        new EmailAddress(recipient.Email),
                        message.Subject,
                        message.Body);

                correspondence.MessageId = messageId;
                correspondence.RecipientId = recipient.Id;

                _correspondenceRepository.Add(correspondence);
            }

            await _correspondenceRepository.UnitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(Show), new { messageId }, "Hello!");
        }


        /// <summary>
        /// Saves the message to the database.
        /// </summary>
        /// <param name="subject">Short description of the message</param>
        /// <param name="body">Main content of the message</param>
        /// <returns>Identity number that stored in DB</returns>
        private async Task<int> SaveMessage(string subject, string body)
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

        /// <summary>
        /// Saves email addresses of recipients to the database.
        /// And returns his ids that stored in DB.
        /// </summary>
        /// <param name="recipientMails">Email addresses of recipients</param>
        /// <returns>List recipients with id and mail</returns>
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

        /// <summary>
        /// Sends message to the recipient and process result of operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipient"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private async Task<Correspondence> SendMessage(EmailAddress sender, EmailAddress recipient, string subject, string body)
        {
            var correspondence = new Correspondence();

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

        /// <summary>
        /// Returns a list of all sent messages including correspondence result information 
        /// </summary>
        /// <returns>List of all sent messages</returns>
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

        /// <summary>
        /// Gets information about sent message including correspondence result
        /// </summary>
        /// <param name="messageId">Sent message id</param>
        /// <returns>Information about sent message</returns>
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

        /// <summary>
        /// Gets result of correspondence by sent message
        /// </summary>
        /// <param name="messageId">Id of sent message</param>
        /// <returns>Result of correspondence by sent message</returns>
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
