using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_STORE.Migrations
{
    /// <inheritdoc />
    public partial class CountAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Sneakers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");
        }
    }
}
