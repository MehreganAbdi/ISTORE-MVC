using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class OffAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OFF",
                table: "Sneakers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OFF",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OFF",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "OFF",
                table: "Products");
        }
    }
}
