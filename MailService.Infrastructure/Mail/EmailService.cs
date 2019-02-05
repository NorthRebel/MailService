using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailService.Services.Mail;

namespace MailService.Infrastructure.Mail
{
    /// <summary>
    /// Implementation of <see cref="IEmailService"/> using MailKit library.
    /// </summary>
    public sealed class EmailService : IEmailService
    {
        /// <inheritdoc cref="IEmailConfiguration"/>
        private readonly IEmailConfiguration _configuration;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration"><inheritdoc cref="IEmailConfiguration"/></param>
        public EmailService(IEmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public async Task SendAsync(EmailMessage message)
        {
            MimeMessage msg = CreateMessage(message);

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(_configuration.Server, _configuration.Port, true);

                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_configuration.UserName, _configuration.Password);

                await emailClient.SendAsync(msg);

                emailClient.Disconnect(true);
            }
        }
        
        /// <summary>
        /// Creates and configures mime message with plain text.
        /// </summary>
        /// <param name="message">Message with sender and recipient information</param>
        /// <returns>MIME message</returns>
        private MimeMessage CreateMessage(EmailMessage message)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress(
                message.Sender.Name, 
                message.Sender.Address
                ));

            msg.To.Add(new MailboxAddress(
                message.Recipient.Name, 
                message.Recipient.Address
                ));

            msg.Subject = message.Subject;
            msg.Body = new TextPart(TextFormat.Plain)
            {
                Text = message.Content
            };

            return msg;
        }
    }
}
