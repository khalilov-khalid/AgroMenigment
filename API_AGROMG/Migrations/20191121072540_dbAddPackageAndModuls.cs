using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class dbAddPackageAndModuls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professions_Companies_CompanyId",
                table: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Professions_CompanyId",
                table: "Professions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Professions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Professions",
                newName: "Key");

            migrationBuilder.AddColumn<int>(
                name: "HumanCount",
                table: "Companies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Moduls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    HumanCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageModuls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackageId = table.Column<int>(nullable: true),
                    ModulId = table.Column<int>(nullable: true)
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
                name: "IX_Companies_PackageId",
                table: "Companies",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageModuls_ModulId",
                table: "PackageModuls",
                column: "ModulId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageModuls_PackageId",
                table: "PackageModuls",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Packages_PackageId",
                table: "Companies",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Packages_PackageId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "PackageModuls");

            migrationBuilder.DropTable(
                name: "Moduls");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Companies_PackageId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HumanCount",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Professions",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Professions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professions_CompanyId",
                table: "Professions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professions_Companies_CompanyId",
                table: "Professions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
