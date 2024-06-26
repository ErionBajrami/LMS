using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommercePersistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    ReportsTo = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DefaultDays = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateJoined", "DateOfBirth", "Firstname", "Lastname", "Position", "ReportsTo" },
                values: new object[,]
                {
                    { "1", new DateTime(2010, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "John", "Doe", "Manager", null },
                    { "2", new DateTime(2015, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Jane", "Smith", "Developer", "1" }
                });

            migrationBuilder.InsertData(
                table: "LeaveType",
                columns: new[] { "Id", "DateCreated", "DefaultDays", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220), 0, "Annual" },
                    { 2, new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220), 20, "Sick" },
                    { 3, new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220), 0, "Replacement" },
                    { 4, new DateTime(2024, 6, 26, 13, 47, 3, 825, DateTimeKind.Utc).AddTicks(220), 10, "Unpaid" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveType");
        }
    }
}
