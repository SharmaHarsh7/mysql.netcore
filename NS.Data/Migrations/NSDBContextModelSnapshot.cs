﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using NS.Data.Models;
using System;

namespace NS.Data.Migrations
{
    [DbContext(typeof(NSDBContext))]
    partial class NSDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("NS.Domain.Models.Users.Employee", b =>
                {
                    b.Property<int>("ID_Employee")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ID_User");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID_Employee");

                    b.HasIndex("ID_User");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("NS.Domain.Models.Users.User", b =>
                {
                    b.Property<int>("ID_User")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("ID_User");

                    b.ToTable("User");
                });

            modelBuilder.Entity("NS.Domain.Models.Users.Employee", b =>
                {
                    b.HasOne("NS.Domain.Models.Users.User", "User")
                        .WithMany("Employees")
                        .HasForeignKey("ID_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
