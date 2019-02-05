using System.Collections.Generic;

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
        /// <param name="fromAddress">Address with which the message will be sent</param>
        /// <param name="subject">Short description of the message</param>
        /// <param name="content">Main content of the message</param>
        /// <param name="toAddresses">List of recipients which who will receive the message</param>
        public EmailMessage(EmailAddress fromAddress, string subject, string content, List<EmailAddress> toAddresses)
        {
            FromAddress = fromAddress;
            Subject = subject;
            Content = content;
            ToAddresses = toAddresses;
        }

        /// <summary>
        /// Constructor without set recipients
        /// </summary>
        /// <param name="fromAddress">Address with which the message will be sent</param>
        /// <param name="subject">Short description of the message</param>
        /// <param name="content">Main content of the message</param>
        public EmailMessage(EmailAddress fromAddress, string subject, string content) 
            : this(fromAddress, subject, content, new List<EmailAddress>())
        {
        }

        /// <summary>
        /// List of recipients which who will receive the message.
        /// </summary>
        public List<EmailAddress> ToAddresses { get; set; }

        /// <summary>
        /// Address with which the message will be sent.
        /// </summary>
        public EmailAddress FromAddress { get; }

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
