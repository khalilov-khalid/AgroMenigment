using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editlangcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "LanguageContexts");

            migrationBuilder.AddColumn<string>(
                name: "LangUnicode",
                table: "LanguageContexts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LangUnicode",
                table: "LanguageContexts");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "LanguageContexts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
