using Microsoft.EntityFrameworkCore;

namespace MailService.Persistence.Infrastructure
{
    public sealed class MailServiceDbContextFactory : DesignTimeDbContextFactoryBase<MailServiceDbContext>
    {
        protected override MailServiceDbContext CreateNewInstance(DbContextOptions<MailServiceDbContext> options)
        {
            return new MailServiceDbContext(options);
        }
    }
}
