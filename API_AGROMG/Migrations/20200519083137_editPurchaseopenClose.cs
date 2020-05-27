using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editPurchaseopenClose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedAmountForCustoms",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EstimatedAmountForGoods",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "EstimatedAmountForTransport",
                table: "Purchases");

            migrationBuilder.AddColumn<bool>(
                name: "OpenClose",
                table: "Purchases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Purchases",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenClose",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Purchases");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedAmountForCustoms",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedAmountForGoods",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedAmountForTransport",
                table: "Purchases",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
