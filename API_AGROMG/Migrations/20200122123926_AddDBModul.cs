using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class AddDBModul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameKey",
                table: "Packets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameKey",
                table: "Packets");
        }
    }
}
