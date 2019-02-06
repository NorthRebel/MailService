using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MailService.API.Models
{
    /// <summary>
    /// Information about message delivery to the recipient.
    /// </summary>
    public class RecipientCorrespondence
    {
        /// <summary>
        /// Email of the recipient.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Date of the message was sent to the recipient.
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <inheritdoc cref="CorrespondenceResult"/>
        [JsonConverter(typeof(StringEnumConverter))]
        public CorrespondenceResult Result { get; set; }

        /// <summary>
        /// Error description if an error occurred during message delivery.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
