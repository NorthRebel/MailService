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
        /// <summary>
        /// Serves a set of objects that are modified in a business transaction (business action)
        /// and manages the recording of changes and the resolution of data contention problems.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
    }
}
