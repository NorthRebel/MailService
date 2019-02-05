using System.Threading.Tasks;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories.Common;
using MailService.Persistence.EntityConfigurations;

namespace MailService.Persistence
{
    public sealed class MailServiceDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<Correspondence> Correspondences { get; set; }

        public MailServiceDbContext(DbContextOptions<MailServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CorrespondenceEntityTypeConfiguration());
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
