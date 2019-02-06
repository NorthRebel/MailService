namespace MailService.Services.Mail
{
    /// <summary>
    /// Address and additional credentials to send email message
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Creates user credentials that contain only address
        /// </summary>
        /// <param name="address">Email address</param>
        public EmailAddress(string address)
        {
            Address = address;
        }

        /// <summary>
        /// Creates user credentials that contain full information
        /// </summary>
        /// <param name="address">Email address</param>
        public EmailAddress(string address, string name)
        {
            Address = address;
            Name = name;
        }

        /// <summary>
        /// Name of user
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Email address
        /// </summary>
        public string Address { get; }
    }
}
