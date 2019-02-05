namespace MailService.Domain.Entities
{
    /// <summary>
    /// Result of sending message from sender to the <see cref="Recipient"/>.
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
