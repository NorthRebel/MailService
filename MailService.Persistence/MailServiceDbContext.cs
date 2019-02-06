using System.Threading.Tasks;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MailService.Domain.Repositories.Common;
using MailService.Persistence.EntityConfigurations;

namespace MailService.Persistence
{
    /// <summary>
    /// EF Core database context for manipulate them.
    /// </summary>
    public sealed class MailServiceDbContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Set of records from database "<see cref="Messages"/>" table.
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Set of records from database "<see cref="Recipient"/>" table.
        /// </summary>
        public DbSet<Recipient> Recipients { get; set; }

        /// <summary>
        /// Set of records from database "<see cref="Correspondence"/>" table.
        /// </summary>
        public DbSet<Correspondence> Correspondences { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options">Options to configure database context</param>
        public MailServiceDbContext(DbContextOptions<MailServiceDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Configures entities that stored in database.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific to a given database.
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipientEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CorrespondenceEntityTypeConfiguration());
        }

        /// <inheritdoc />
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
