namespace MailService.Services.Mail
{
    /// <summary>
    /// Configuration params for SMTP/POP3/etc client to work with <see cref="IEmailService"/>.
    /// </summary>
    public interface IEmailConfiguration
    {
        /// <summary>
        /// URL of server
        /// </summary>
        /// <example>
        /// smtp.myserver.com
        /// </example>
        string Server { get; set; }

        /// <summary>
        /// Port of server
        /// </summary>
        /// <example>
        /// 465
        /// </example>
        int Port { get; set; }

        /// <summary>
        /// Email address to authenticate mail client
        /// Messages will be sent through this address
        /// </summary>
        /// <example>
        /// login@yandex.ru
        /// </example>
        string UserName { get; set; }

        /// <summary>
        /// Password to authenticate mail client
        /// </summary>
        string Password { get; set; }
    }
}
