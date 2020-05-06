using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class FerKindLang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FertilizerKindLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    FertilizerKindId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerKindLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FertilizerKindLanguage_FertilizerKind_FertilizerKindId",
                        column: x => x.FertilizerKindId,
                        principalTable: "FertilizerKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FertilizerKindLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerKindLanguage_FertilizerKindId",
                table: "FertilizerKindLanguage",
                column: "FertilizerKindId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerKindLanguage_LanguageId",
                table: "FertilizerKindLanguage",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FertilizerKindLanguage");
        }
    }
}
