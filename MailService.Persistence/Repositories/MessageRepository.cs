using System.Threading.Tasks;
using System.Collections.Generic;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories;
using MailService.Domain.Repositories.Common;

namespace MailService.Persistence.Repositories
{
    public sealed class MessageRepository : IMessageRepository
    {
        private readonly MailServiceDbContext _context;

        public MessageRepository(MailServiceDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Message Add(Message message)
        {
            return _context.Messages.Add(message).Entity;
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            Message[] messages = await _context.Messages.ToArrayAsync();

            return messages;
        }
    }
}
