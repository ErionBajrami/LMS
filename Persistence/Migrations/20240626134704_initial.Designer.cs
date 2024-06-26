﻿// <auto-generated />
using System;
using EcommercePersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EcommercePersistence.Migrations
{
    [DbContext(typeof(DatabaseService))]
    [Migration("20240626134704_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EcommerceDomain.LeaveTypes.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DefaultDays")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LeaveType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220),
                            DefaultDays = 0,
                            Name = "Annual"
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220),
                            DefaultDays = 20,
                            Name = "Sick"
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220),
                            DefaultDays = 0,
                            Name = "Replacement"
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220),
                            DefaultDays = 10,
                            Name = "Unpaid"
                        });
                });

            modelBuilder.Entity("LMS.Domain.Employees.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("Position")
                        .HasColumnType("text");

                    b.Property<string>("ReportsTo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            DateJoined = new DateTime(2010, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            DateOfBirth = new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc),
                            Firstname = "John",
                            Lastname = "Doe",
                            Position = "Manager"
                        },
                        new
                        {
                            Id = "2",
                            DateJoined = new DateTime(2015, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc),
                            DateOfBirth = new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc),
                            Firstname = "Jane",
                            Lastname = "Smith",
                            Position = "Developer",
                            ReportsTo = "1"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
