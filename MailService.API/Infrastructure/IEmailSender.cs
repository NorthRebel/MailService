namespace MailService.API.Infrastructure
{
    /// <summary>
    /// Credentials that used to send messages.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Name of user
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        string Address { get; set; }
    }
}
