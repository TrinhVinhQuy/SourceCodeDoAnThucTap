using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.DATA.Migrations
{
    /// <inheritdoc />
    public partial class updateTbNew_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewTest",
                table: "New");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewTest",
                table: "New",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
