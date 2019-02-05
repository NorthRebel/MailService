using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailService.Persistence.EntityConfigurations
{
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
