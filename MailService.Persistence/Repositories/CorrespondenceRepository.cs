using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Domain.Entities;
using MailService.Domain.Repositories;
using MailService.Domain.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace MailService.Persistence.Repositories
{
    public sealed class CorrespondenceRepository : ICorrespondenceRepository
    {
        private readonly MailServiceDbContext _context;

        public CorrespondenceRepository(MailServiceDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Correspondence correspondence)
        {
            _context.Correspondences.Add(correspondence);
        }

        public async Task<IEnumerable<Correspondence>> GetAllByMessageId(int messageId)
        {
            Correspondence[] correspondences = await _context.Correspondences
                                                             .Where(c => c.MessageId == messageId)
                                                             .ToArrayAsync();

            return correspondences;
        }
    }
}
