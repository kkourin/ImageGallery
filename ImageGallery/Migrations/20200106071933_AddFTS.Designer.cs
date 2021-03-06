﻿// <auto-generated />
using System;
using ImageGallery.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageGallery.Migrations
{
    [DbContext(typeof(FilesContext))]
    [Migration("20200106071933_AddFTS")]
    partial class AddFTS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("ImageGallery.Database.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Custom_fts")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Directory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Directory_fts")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Extension_fts")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<DateTime?>("FileCreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUseTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name_fts")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<string>("Name_tokenized_fts")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<byte[]>("Thumbnail")
                        .HasColumnType("BLOB");

                    b.Property<int>("TimesAccessed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<int>("WatcherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CreatedTime");

                    b.HasIndex("Directory");

                    b.HasIndex("FileCreatedTime");

                    b.HasIndex("FullName");

                    b.HasIndex("LastUseTime");

                    b.HasIndex("TimesAccessed");

                    b.HasIndex("WatcherId");

                    b.HasIndex("FullName", "WatcherId")
                        .IsUnique();

                    b.ToTable("Files");
                });

            modelBuilder.Entity("ImageGallery.Database.Models.Watcher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Directory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Whitelist")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.HasKey("Id");

                    b.ToTable("Watchers");
                });

            modelBuilder.Entity("ImageGallery.Database.Models.File", b =>
                {
                    b.HasOne("ImageGallery.Database.Models.Watcher", "Watcher")
                        .WithMany("Files")
                        .HasForeignKey("WatcherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
