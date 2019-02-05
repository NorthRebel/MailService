﻿using MailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailService.Persistence.EntityConfigurations
{
    internal sealed class RecipientEntityTypeConfiguration : IEntityTypeConfiguration<Recipient>
    {
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            builder.Property(e => e.Id)
                .HasColumnName("RecipientId");

            builder.Property(e => e.Email)
                .HasMaxLength(255);
        }
    }
}
