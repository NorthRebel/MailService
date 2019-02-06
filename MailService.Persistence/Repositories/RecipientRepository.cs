using System.Threading.Tasks;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories;
using MailService.Domain.Repositories.Common;

namespace MailService.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IRecipientRepository"/> using EF Core.
    /// </summary>
    public sealed class RecipientRepository : IRecipientRepository
    {
        private readonly MailServiceDbContext _context;

        /// <summary>
        /// Default constructor with database context injection.
        /// </summary>
        /// <param name="context">EF Core database context</param>
        public RecipientRepository(MailServiceDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public IUnitOfWork UnitOfWork => _context;

        /// <inheritdoc />
        public Recipient Add(Recipient recipient)
        {
            return _context.Recipients.Add(recipient).Entity;
        }

        /// <inheritdoc />
        public async Task<Recipient> GetById(int id)
        {
            Recipient recipient = await _context.Recipients.FindAsync(id);

            return recipient;
        }

        /// <inheritdoc />
        public async Task<Recipient> GetByEmail(string email)
        {
            Recipient recipient = await _context.Recipients.SingleOrDefaultAsync(r => r.Email == email);

            return recipient;
        }
    }
}
