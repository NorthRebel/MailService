using System;
using System.Threading.Tasks;

namespace MailService.Domain.Repositories.Common
{
    /// <summary>
    /// Serves a set of objects that are modified in a business transaction (business action)
    /// and manages the recording of changes and the resolution of data contention problems.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all changes of repository
        /// </summary>
        /// <returns>Number of processed entities</returns>
        Task<int> SaveChangesAsync();
    }
}
