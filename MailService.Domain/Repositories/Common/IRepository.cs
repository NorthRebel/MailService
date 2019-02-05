using MailService.Domain.Entities.Common;

namespace MailService.Domain.Repositories.Common
{
    /// <summary>
    /// Encapsulates the objects represented in the data warehouse and the operations performed on them,
    /// providing a more object-oriented representation of real data.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity which belongs to repository</typeparam>
    public interface IRepository<TEntity> where TEntity: IBaseEntity
    {
        /// <inheritdoc cref="IUnitOfWork"/>
        IUnitOfWork UnitOfWork { get; }
    }
}
