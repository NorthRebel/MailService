using System;
using System.Collections.Generic;
using MailService.Domain.Entities.Common;

namespace MailService.Domain.Entities
{
    /// <summary>
    /// Entity of recipient, which receives a message.
    /// </summary>
    public class Recipient : IEntity<int>
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Email of the recipient.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// List of exchange results of message between sender.
        /// </summary>
        public ICollection<Correspondence> Correspondences { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// Needs to initialize the collection recipient to avoid <see cref="NullReferenceException"/> while access to <see cref="Correspondences"/> property.
        /// </remarks>
        public Recipient()
        {
            Correspondences = new HashSet<Correspondence>();
        }
    }
}
