namespace MailService.Domain.Common
{
    /// <summary>
    /// Adds necessary identity property for entity.
    /// </summary>
    /// <typeparam name="TKey">Type of entity identity property.</typeparam>
    public interface IEntity<TKey> : IBaseEntity where TKey: new()
    {
        /// <summary>
        /// Identity property of entity.
        /// </summary>
        TKey Id { get; set; }
    }
}
