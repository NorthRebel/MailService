using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailService.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configuration of <see cref="Message"/> entity that used <see cref="MailServiceDbContext"/>
    /// </summary>
    internal sealed class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("MessageId");

            builder.Property(e => e.Subject)
                .HasMaxLength(255);
        }
    }
}
