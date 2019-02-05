using System.Threading.Tasks;

namespace MailService.Services.Mail
{
    /// <summary>
    /// Service to work with mail.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Send <see cref="EmailMessage"/> to recipients.
        /// </summary>
        /// <param name="message">Message to send</param>
        Task SendAsync(EmailMessage message);
    }
}
