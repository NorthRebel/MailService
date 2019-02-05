using System;
using System.Collections.Generic;
using MailService.Domain.Entities.Common;

namespace MailService.Domain.Entities
{
    /// <summary>
    /// Entity of mail message, which sent to <see cref="Recipient"/>.
    /// </summary>
    public class Message : IEntity<int>
    {
        /// <inheritdoc />
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
        /// List of exchange results of message between sender and <see cref="Recipient"/>.
        /// </summary>
        public ICollection<Correspondence> Correspondences { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// Needs to initialize the collection recipient to avoid <see cref="NullReferenceException"/> while access to <see cref="Correspondences"/> property.
        /// </remarks>
        public Message()
        {
            Correspondences = new HashSet<Correspondence>();
        }
    }
}
