using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Governorate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isHandled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "City", "CreationDateTime", "District", "Governorate", "PhoneNumber", "isHandled" },
                values: new object[,]
                {
                    { 1, "City1", new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "District1", "Governorate1", "1234567890", false },
                    { 2, "City2", new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "District2", "Governorate2", "0987654321", true },
                    { 3, "City3", new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "District3", "Governorate3", "0987784321", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
