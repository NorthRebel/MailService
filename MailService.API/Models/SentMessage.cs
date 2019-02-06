using System;
using System.Collections.Generic;

namespace MailService.API.Models
{
    /// <summary>
    /// Message that was sent earlier.
    /// </summary>
    public class SentMessage
    {
        /// <summary>
        /// Id of message which contains in database.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Short description of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Main content of the message.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Date of message creation.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// List of exchange results of message between sender and recipient.
        /// </summary>
        public ICollection<RecipientCorrespondence> Correspondences { get; set; }
    }
}
