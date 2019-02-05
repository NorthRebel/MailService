using System.Threading.Tasks;
using System.Collections.Generic;
using MailService.Domain.Entities;
using MailService.Domain.Repositories.Common;

namespace MailService.Domain.Repositories
{
    /// <summary>
    /// Repository for manage set of <see cref="Correspondence"/> entities.
    /// </summary>
    public interface ICorrespondenceRepository : IRepository<Correspondence>
    {
        /// <summary>
        /// Adds new correspondence to data storage
        /// </summary>
        /// <param name="correspondence">New instance of correspondence</param>
        void Add(Correspondence correspondence);

        /// <summary>
        /// Gets all correspondences by id of sent message
        /// </summary>
        /// <param name="messageId">Id of sent message</param>
        /// <returns>List of correspondences of the message</returns>
        Task<IEnumerable<Correspondence>> GetAllByMessageId(int messageId);
    }
}
