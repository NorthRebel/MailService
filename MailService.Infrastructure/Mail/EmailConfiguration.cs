using MailService.Services.Mail;

namespace MailService.Infrastructure.Mail
{
    /// <summary>
    /// Configuration params for SMTP/POP3/etc client to work with <see cref="IEmailService"/>.
    /// </summary>
    public class EmailConfiguration : IEmailConfiguration
    {
        /// <inheritdoc />
        public string Server { get; set; }

        /// <inheritdoc />
        public int Port { get; set; }

        /// <inheritdoc />
        public string UserName { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }
    }
}
