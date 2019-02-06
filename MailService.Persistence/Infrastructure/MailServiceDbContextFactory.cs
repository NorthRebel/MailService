using Microsoft.EntityFrameworkCore;

namespace MailService.Persistence.Infrastructure
{
    /// <summary>
    /// Database context factory implementation for <see cref="MailServiceDbContext"/>
    /// This helps create migrations from other library.
    /// </summary>
    public sealed class MailServiceDbContextFactory : DesignTimeDbContextFactoryBase<MailServiceDbContext>
    {
        protected override MailServiceDbContext CreateNewInstance(DbContextOptions<MailServiceDbContext> options)
        {
            return new MailServiceDbContext(options);
        }
    }
}
