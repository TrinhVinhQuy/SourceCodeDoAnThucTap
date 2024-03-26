using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.DATA.Migrations
{
    /// <inheritdoc />
    public partial class update_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "User",
                newName: "Town");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Town",
                table: "User",
                newName: "City");

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "User",
                type: "int",
                nullable: true);
        }
    }
}
