using System;
using MailService.Domain.Common;

namespace MailService.Domain
{
    /// <summary>
    /// Entity of correspondence in which the exchange of message takes place between sender and <see cref="Domain.Recipient"/>.
    /// </summary>
    public class Correspondence : IBaseEntity
    {
        /// <summary>
        /// Identity property of <see cref="Message"/> entity.
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// Identity property of <see cref="Recipient"/> entity.  
        /// </summary>
        public int RecipientId { get; set; }

        /// <summary>
        /// Date of the message was sent to the <see cref="Domain.Recipient"/>.
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <inheritdoc cref="Domain.CorrespondenceResult"/>
        public CorrespondenceResult Result { get; set; }

        /// <summary>
        /// Error description if an error occurred during message delivery
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <inheritdoc cref="Domain.Message"/>
        public Message Message { get; set; }

        /// <inheritdoc cref="Domain.Recipient"/>
        public Recipient Recipient { get; set; }
    }
}
