using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_STORE.Migrations
{
    /// <inheritdoc />
    public partial class ImagePropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Sneakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sneakers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");
        }
    }
}
