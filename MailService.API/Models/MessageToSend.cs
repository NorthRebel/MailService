using System.Collections.Generic;

namespace MailService.API.Models
{
    /// <summary>
    /// Message to send to the recipient.
    /// </summary>
    public class MessageToSend
    {
        /// <summary>
        /// Short description of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Main content of the message.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// List of recipients which will exchange message from sender.
        /// </summary>
        public IEnumerable<string> Recipients { get; set; }
    }
}
