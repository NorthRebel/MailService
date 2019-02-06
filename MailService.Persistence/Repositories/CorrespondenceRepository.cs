using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories;
using MailService.Domain.Repositories.Common;

namespace MailService.Persistence.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ICorrespondenceRepository"/> using EF Core.
    /// </summary>
    public sealed class CorrespondenceRepository : ICorrespondenceRepository
    {
        private readonly MailServiceDbContext _context;

        /// <summary>
        /// Default constructor with database context injection.
        /// </summary>
        /// <param name="context">EF Core database context</param>
        public CorrespondenceRepository(MailServiceDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public IUnitOfWork UnitOfWork => _context;

        /// <inheritdoc />
        public void Add(Correspondence correspondence)
        {
            _context.Correspondences.Add(correspondence);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Correspondence>> GetAllByMessageId(int messageId)
        {
            Correspondence[] correspondences = await _context.Correspondences
                                                             .Where(c => c.MessageId == messageId)
                                                             .ToArrayAsync();

            return correspondences;
        }
    }
}
