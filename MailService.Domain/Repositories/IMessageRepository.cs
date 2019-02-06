using System.Threading.Tasks;
using System.Collections.Generic;
using MailService.Domain.Entities;
using MailService.Domain.Repositories.Common;

namespace MailService.Domain.Repositories
{
    /// <summary>
    /// Repository for manage set of <see cref="Message"/> entities.
    /// </summary>
    public interface IMessageRepository : IRepository<Message>
    {
        /// <summary>
        /// Adds new message to data storage
        /// </summary>
        /// <param name="message">New instance of message</param>
        /// <returns>Current instance of message with new id</returns>
        Message Add(Message message);

        /// <summary>
        /// Gets all sent messages
        /// </summary>
        /// <returns>List of sent messages</returns>
        Task<IEnumerable<Message>> GetAll();

        /// <summary>
        /// Get sent message by id
        /// </summary>
        /// <param name="id">Id of sent message</param>
        /// <returns>Instance of sent message</returns>
        Task<Message> GetById(int id);
    }
}
