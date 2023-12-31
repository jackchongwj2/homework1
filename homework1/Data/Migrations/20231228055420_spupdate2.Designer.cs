﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using homework1.Data;

#nullable disable

namespace homework1.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20231228055420_spupdate2")]
    partial class spupdate2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("homework1.Models.Result", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Marks")
                        .HasColumnType("numeric(4,1)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("ResultId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("homework1.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("homework1.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(8)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("homework1.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("homework1.ViewModels.SubjectViewModel", b =>
                {
                    b.Property<string>("SubjectCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("TeacherName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("SubjectViewModel");
                });

            modelBuilder.Entity("homework1.Models.Result", b =>
                {
                    b.HasOne("homework1.Models.Student", "Student")
                        .WithMany("Results")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("homework1.Models.Subject", "Subject")
                        .WithMany("Results")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("homework1.Models.Subject", b =>
                {
                    b.HasOne("homework1.Models.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("homework1.Models.Student", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("homework1.Models.Subject", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("homework1.Models.Teacher", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
