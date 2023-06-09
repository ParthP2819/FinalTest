﻿// <auto-generated />
using FinalTest.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalTest.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230426002636_addtoall")]
    partial class addtoall
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FinalTest.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdminEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AdminPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AdminId");

                    b.ToTable("admins");
                });

            modelBuilder.Entity("FinalTest.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("CoursePrice")
                        .HasColumnType("double");

                    b.HasKey("CourseId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("FinalTest.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("ContactNo")
                        .HasColumnType("double");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RollNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Zipcode")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("FinalTest.Models.StudentCourses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("studentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
