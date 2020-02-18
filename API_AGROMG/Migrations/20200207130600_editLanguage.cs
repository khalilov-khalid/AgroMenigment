using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code2",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iso",
                table: "Languages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "code2",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "iso",
                table: "Languages");
        }
    }
}
