namespace MailService.API.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
