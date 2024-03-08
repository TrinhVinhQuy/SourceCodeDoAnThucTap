using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.DATA.Migrations
{
    /// <inheritdoc />
    public partial class tableOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradingCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TradingCode",
                table: "Order");
        }
    }
}
