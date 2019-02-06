using System.Threading.Tasks;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories;
using MailService.Domain.Repositories.Common;

namespace MailService.Persistence.Repositories
{
    public sealed class RecipientRepository : IRecipientRepository
    {
        private readonly MailServiceDbContext _context;

        public RecipientRepository(MailServiceDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Recipient Add(Recipient recipient)
        {
            return _context.Recipients.Add(recipient).Entity;
        }

        public async Task<Recipient> GetById(int id)
        {
            Recipient recipient = await _context.Recipients.FindAsync(id);

            return recipient;
        }

        public async Task<Recipient> GetByEmail(string email)
        {
            Recipient recipient = await _context.Recipients.SingleOrDefaultAsync(r => r.Email == email);

            return recipient;
        }
    }
}
