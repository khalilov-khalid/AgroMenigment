using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class createGaragedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechniqueCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    IsTrailer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechniqueCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Techniques",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    ProductionYear = table.Column<DateTime>(nullable: false),
                    EnginePower = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    DateOfPurchase = table.Column<DateTime>(nullable: false),
                    EngineNumber = table.Column<string>(nullable: true),
                    TechnicalCondition = table.Column<int>(nullable: false),
                    IsBusy = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    TechniqueCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Techniques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Techniques_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Techniques_TechniqueCategories_TechniqueCategoryId",
                        column: x => x.TechniqueCategoryId,
                        principalTable: "TechniqueCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Techniques_CompanyId",
                table: "Techniques",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Techniques_TechniqueCategoryId",
                table: "Techniques",
                column: "TechniqueCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Techniques");

            migrationBuilder.DropTable(
                name: "TechniqueCategories");
        }
    }
}
