using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_STORE.Migrations
{
    /// <inheritdoc />
    public partial class priceadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Sneakers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");
        }
    }
}
