namespace MailService.API.Models
{
    /// <summary>
    /// Result of sending message from sender to the recipient.
    /// </summary>
    public enum CorrespondenceResult : byte
    {
        /// <summary>
        /// Message successfully delivered
        /// </summary>
        Ok,

        /// <summary>
        /// An error has occurred during message delivery
        /// </summary>
        Failed
    }
}
