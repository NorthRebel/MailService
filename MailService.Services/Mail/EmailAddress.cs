namespace MailService.Services.Mail
{
    /// <summary>
    /// Address and additional credentials to send email message
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="address">Email address</param>
        public EmailAddress(string address)
        {
            Address = address;
        }

        /// <summary>
        /// Name of user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Address { get; set; }
    }
}
