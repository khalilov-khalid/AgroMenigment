using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class HRWorkerFinSeriaSSN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fin",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Workers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fin",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Workers");
        }
    }
}
