using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_STORE.Migrations
{
    /// <inheritdoc />
    public partial class SneakersAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumericSize",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "AlphabeticalSize",
                table: "Products",
                newName: "Size");

            migrationBuilder.CreateTable(
                name: "Sneakers",
                columns: table => new
                {
                    SneakerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sneakers", x => x.SneakerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sneakers");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Products",
                newName: "AlphabeticalSize");

            migrationBuilder.AddColumn<int>(
                name: "NumericSize",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
