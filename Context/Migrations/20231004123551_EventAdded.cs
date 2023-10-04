using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class EventAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Sneakers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeRemaining = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sneakers_EventId",
                table: "Sneakers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_EventId",
                table: "Products",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Events_EventId",
                table: "Products",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sneakers_Events_EventId",
                table: "Sneakers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Events_EventId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sneakers_Events_EventId",
                table: "Sneakers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Sneakers_EventId",
                table: "Sneakers");

            migrationBuilder.DropIndex(
                name: "IX_Products_EventId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Products");
        }
    }
}
