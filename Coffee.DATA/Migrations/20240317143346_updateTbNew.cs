using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.DATA.Migrations
{
    /// <inheritdoc />
    public partial class updateTbNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NewTest",
                table: "New",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category",
                table: "Product",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NewTest",
                table: "New");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category",
                table: "Product",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
