using System;
using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailService.Persistence.EntityConfigurations
{
    internal sealed class CorrespondenceEntityTypeConfiguration : IEntityTypeConfiguration<Correspondence>
    {
        public void Configure(EntityTypeBuilder<Correspondence> builder)
        {
            builder.HasKey(e => new
            {
                e.MessageId,
                e.RecipientId
            });

            builder.Property(e => e.Result)
                .HasConversion(
                    r => r.ToString(),
                    r => (CorrespondenceResult)Enum.Parse(typeof(CorrespondenceResult), r));

            builder.HasOne(e => e.Message)
                .WithMany(e => e.Correspondences)
                .HasForeignKey(e => e.MessageId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Correspondences_Message");

            builder.HasOne(e => e.Recipient)
                .WithMany(e => e.Correspondences)
                .HasForeignKey(e => e.RecipientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Correspondences_Recipient");
        }
    }
}
