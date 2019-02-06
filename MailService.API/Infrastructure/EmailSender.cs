namespace MailService.API.Infrastructure
{
    /// <summary>
    /// Credentials that used to send messages.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Address { get; set; }
    }
}
