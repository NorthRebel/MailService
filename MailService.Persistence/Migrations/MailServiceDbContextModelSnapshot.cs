﻿// <auto-generated />
using System;
using MailService.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MailService.Persistence.Migrations
{
    [DbContext(typeof(MailServiceDbContext))]
    partial class MailServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MailService.Domain.Entities.Correspondence", b =>
                {
                    b.Property<int>("MessageId");

                    b.Property<int>("RecipientId");

                    b.Property<string>("ErrorMessage");

                    b.Property<string>("Result")
                        .IsRequired();

                    b.Property<DateTime>("SendDate");

                    b.HasKey("MessageId", "RecipientId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Correspondences");
                });

            modelBuilder.Entity("MailService.Domain.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("MessageId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Subject")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MailService.Domain.Entities.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RecipientId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("IX_Email")
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("MailService.Domain.Entities.Correspondence", b =>
                {
                    b.HasOne("MailService.Domain.Entities.Message", "Message")
                        .WithMany("Correspondences")
                        .HasForeignKey("MessageId")
                        .HasConstraintName("FK_Correspondences_Message")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MailService.Domain.Entities.Recipient", "Recipient")
                        .WithMany("Correspondences")
                        .HasForeignKey("RecipientId")
                        .HasConstraintName("FK_Correspondences_Recipient")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
