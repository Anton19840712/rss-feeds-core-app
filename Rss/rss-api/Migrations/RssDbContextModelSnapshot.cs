﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using rss_api.Contexts;

#nullable disable

namespace rss_api.Migrations
{
    [DbContext(typeof(RssDbContext))]
    partial class RssDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("rss_api.Models.Dal.RssDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(30000)
                        .HasColumnType("character varying(30000)");

                    b.Property<string>("Header")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("RssDalElementsId")
                        .HasColumnType("uuid");

                    b.Property<NpgsqlTsVector>("SearchVector")
                        .HasColumnType("tsvector");

                    b.Property<string>("Tag")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("Id"), new[] { "Header" });

                    b.HasIndex("RssDalElementsId");

                    b.ToTable("RssFeeds");
                });

            modelBuilder.Entity("rss_api.Models.Dal.RssDalElements", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RssElements");
                });

            modelBuilder.Entity("rss_api.Models.Dal.RssDal", b =>
                {
                    b.HasOne("rss_api.Models.Dal.RssDalElements", null)
                        .WithMany("RssDalItems")
                        .HasForeignKey("RssDalElementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("rss_api.Models.Dal.RssDalElements", b =>
                {
                    b.Navigation("RssDalItems");
                });
#pragma warning restore 612, 618
        }
    }
}
