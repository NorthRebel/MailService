using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailService.Persistence.EntityConfigurations
{
    /// <summary>
    /// Configuration of <see cref="Recipient"/> entity that used <see cref="MailServiceDbContext"/>
    /// </summary>
    internal sealed class RecipientEntityTypeConfiguration : IEntityTypeConfiguration<Recipient>
    {
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("RecipientId");

            builder.Property(e => e.Email)
                .HasMaxLength(255);

            builder.HasIndex(e => e.Email)
                .IsUnique()
                .HasName("IX_Email");
        }
    }
}
