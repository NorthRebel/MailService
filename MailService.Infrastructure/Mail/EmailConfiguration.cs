using MailService.Services.Mail;

namespace MailService.Infrastructure.Mail
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
