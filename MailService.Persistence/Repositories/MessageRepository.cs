using System.Threading.Tasks;
using System.Collections.Generic;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories;
using MailService.Domain.Repositories.Common;

namespace MailService.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IMessageRepository"/> using EF Core.
    /// </summary>
    public sealed class MessageRepository : IMessageRepository
    {
        private readonly MailServiceDbContext _context;

        /// <summary>
        /// Default constructor with database context injection.
        /// </summary>
        /// <param name="context">EF Core database context</param>
        public MessageRepository(MailServiceDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public IUnitOfWork UnitOfWork => _context;

        /// <inheritdoc />
        public Message Add(Message message)
        {
            return _context.Messages.Add(message).Entity;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Message>> GetAll()
        {
            Message[] messages = await _context.Messages.ToArrayAsync();

            return messages;
        }

        /// <inheritdoc />
        public async Task<Message> GetById(int id)
        {
            Message message = await _context.Messages.FindAsync(id);

            return message;
        }
    }
}
