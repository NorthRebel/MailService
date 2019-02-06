namespace MailService.API.Infrastructure
{
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
