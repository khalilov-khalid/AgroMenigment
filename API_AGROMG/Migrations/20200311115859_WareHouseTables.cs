using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class WareHouseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WareHourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MainId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHourses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WareHourses_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MainId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouseCategories_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WareHourses_CompanyId",
                table: "WareHourses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHourses_LanguageId",
                table: "WareHourses",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseCategories_LanguageId",
                table: "WareHouseCategories",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WareHourses");

            migrationBuilder.DropTable(
                name: "WareHouseCategories");
        }
    }
}
