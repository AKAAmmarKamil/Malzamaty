﻿// <auto-generated />
using System;
using Malzamaty.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Malzamaty.Migrations
{
    [DbContext(typeof(TheContext))]
    partial class TheContextModelSnapshot : ModelSnapshot
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

                    b.Property<Guid>("Co_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("S_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("T_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("Co_ID");

                    b.HasIndex("S_ID");

                    b.HasIndex("T_ID");

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

                    b.Property<Guid?>("C_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DownloadCount")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Format")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublishDate")
                        .HasColumnType("int");

                    b.Property<Guid?>("Subject_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("User_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("C_ID");

                    b.HasIndex("Subject_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Malzamaty.Model.Interests", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("C_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Su_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("U_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("C_ID");

                    b.HasIndex("Su_ID");

                    b.HasIndex("U_ID");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("Malzamaty.Model.Match", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("C_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Su_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("C_ID");

                    b.HasIndex("Su_ID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Malzamaty.Model.Rating", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("F_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<Guid?>("Us_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("FileID");

                    b.HasIndex("Us_ID");

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

                    b.Property<Guid>("F_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("F_ID");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("Malzamaty.Model.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Malzamaty.Model.Schedule", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("FinishStudy")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("St_ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartStudy")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("Su_ID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("St_ID");

                    b.HasIndex("Su_ID");

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

                    b.Property<Guid>("Authentication")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Authentication");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Malzamaty.Model.Class", b =>
                {
                    b.HasOne("Malzamaty.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("Co_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("S_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.ClassType", "ClassType")
                        .WithMany()
                        .HasForeignKey("T_ID")
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
                        .HasForeignKey("C_ID");

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("Subject_ID");

                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("User_ID");

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.Interests", b =>
                {
                    b.HasOne("Malzamaty.Model.Class", "Class")
                        .WithMany()
                        .HasForeignKey("C_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("Su_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("U_ID")
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
                        .HasForeignKey("C_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("Su_ID")
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
                        .HasForeignKey("Us_ID");

                    b.Navigation("File");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.Report", b =>
                {
                    b.HasOne("Malzamaty.Model.File", "File")
                        .WithMany()
                        .HasForeignKey("F_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("Malzamaty.Model.Schedule", b =>
                {
                    b.HasOne("Malzamaty.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("St_ID");

                    b.HasOne("Malzamaty.Model.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("Su_ID");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Malzamaty.Model.User", b =>
                {
                    b.HasOne("Malzamaty.Model.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("Authentication")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
