namespace MailService.Services.Mail
{
    /// <summary>
    /// Message which will be sent to the recipient.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="sender">Address with which the message will be sent</param>
        /// <param name="recipient">Address which will receive the message</param>
        /// <param name="subject">Short description of the message</param>
        /// <param name="content">Main content of the message</param>
        public EmailMessage(EmailAddress sender, EmailAddress recipient, string subject, string content)
        {
            Sender = sender;
            Recipient = recipient;
            Subject = subject;
            Content = content;
        }

        /// <summary>
        /// Address which will receive the message.
        /// </summary>
        public EmailAddress Recipient { get; }

        /// <summary>
        /// Address with which the message will be sent.
        /// </summary>
        public EmailAddress Sender { get; }

        /// <summary>
        /// Short description of the message.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Main content of the message.
        /// </summary>
        public string Content { get; }
    }
}
