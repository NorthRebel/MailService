using System.Threading.Tasks;
using MailService.Domain.Entities;
using MailService.Domain.Repositories.Common;

namespace MailService.Domain.Repositories
{
    /// <summary>
    /// Repository for manage set of <see cref="Recipient"/> entities.
    /// </summary>
    public interface IRecipientRepository : IRepository<Recipient>
    {
        /// <summary>
        /// Adds new recipient to data storage
        /// </summary>
        /// <param name="recipient">New instance of recipient</param>
        /// <returns>Current instance of recipient with new id</returns>
        Recipient Add(Recipient recipient);

        /// <summary>
        /// Gets recipient by id
        /// </summary>
        /// <param name="id">Id of recipient</param>
        /// <returns>Instance of recipient</returns>
        Task<Recipient> GetById(int id);
    }
}
