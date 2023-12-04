﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sovos.CaseStudy.Persistence.Contexts;

#nullable disable

namespace Sovos.CaseStudy.Persistence.Migrations
{
    [DbContext(typeof(SovosCaseDbContext))]
    [Migration("20231127050518_dbUpdates")]
    partial class dbUpdates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Sovos.CaseStudy.Domain.Entites.InvoiceHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsMailSended")
                        .HasColumnType("boolean");

                    b.Property<string>("ReceiverTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SendDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("InvoiceHeaders", (string)null);
                });

            modelBuilder.Entity("Sovos.CaseStudy.Domain.Entites.InvoiceLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InvoiceHeaderId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("UnitCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceHeaderId");

                    b.ToTable("Invoice Lines", (string)null);
                });

            modelBuilder.Entity("Sovos.CaseStudy.Domain.Entites.InvoiceLine", b =>
                {
                    b.HasOne("Sovos.CaseStudy.Domain.Entites.InvoiceHeader", null)
                        .WithMany("InvoiceLine")
                        .HasForeignKey("InvoiceHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sovos.CaseStudy.Domain.Entites.InvoiceHeader", b =>
                {
                    b.Navigation("InvoiceLine");
                });
#pragma warning restore 612, 618
        }
    }
}
