using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editpackagedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageModuls");

            migrationBuilder.AddColumn<string>(
                name: "ModulContent",
                table: "Packages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModulContent",
                table: "Packages");

            migrationBuilder.CreateTable(
                name: "PackageModuls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModulId = table.Column<int>(nullable: true),
                    PackageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageModuls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageModuls_Moduls_ModulId",
                        column: x => x.ModulId,
                        principalTable: "Moduls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackageModuls_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageModuls_ModulId",
                table: "PackageModuls",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageModuls_PackageId",
                table: "PackageModuls",
                column: "PackageId");
        }
    }
}
