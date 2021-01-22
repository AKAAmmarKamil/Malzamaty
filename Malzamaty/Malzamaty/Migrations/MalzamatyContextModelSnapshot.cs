﻿// <auto-generated />
using System;
using Malzamaty;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Malzamaty.Migrations
{
    [DbContext(typeof(MalzamatyContext))]
    partial class MalzamatyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Malzamaty.Model.Class", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StageID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ClassTypeID");

                    b.HasIndex("CountryID");

                    b.HasIndex("StageID");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("Malzamaty.Model.ClassType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClassType");
                });

            modelBuilder.Entity("Malzamaty.Model.Country", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("Malzamaty.Model.File", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ClassID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DownloadCount")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublishDate")
                        .HasColumnType("int");

                    b.Property<Guid?>("SubjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UploadDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectID");

                    b.HasIndex("UserID");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Malzamaty.Model.Interests", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("Malzamaty.Model.Match", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Malzamaty.Model.Rating", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("FileID");

                    b.HasIndex("UserID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("Malzamaty.Model.Report", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FileID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("FileID");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("Malzamaty.Model.Schedule", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FinishStudy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartStudy")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SubjectID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("SubjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("Malzamaty.Model.Stage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("Malzamaty.Model.Subject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Malzamaty.Model.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activated")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Malzamaty.Model.Class", b =>
                {
                    b.HasOne("Malzamaty.Model.ClassType", "ClassType")
                        .WithMany()
                        .HasForeignKey("ClassTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassType");

                    b.Navigation("Country");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Malzamaty.Model.File", b =>
                {
                    b.HasOne("Malzamaty.Model.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID");

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID");

                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.Interests", b =>
                {
                    b.HasOne("Malzamaty.Model.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.Match", b =>
                {
                    b.HasOne("Malzamaty.Model.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Malzamaty.Model.Rating", b =>
                {
                    b.HasOne("Malzamaty.Model.File", "File")
                        .WithMany()
                        .HasForeignKey("FileID");

                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("File");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.Report", b =>
                {
                    b.HasOne("Malzamaty.Model.File", "File")
                        .WithMany("Report")
                        .HasForeignKey("FileID");

                    b.Navigation("File");
                });

            modelBuilder.Entity("Malzamaty.Model.Schedule", b =>
                {
                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID");

                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.File", b =>
                {
                    b.Navigation("Report");
                });
#pragma warning restore 612, 618
        }
    }
}
