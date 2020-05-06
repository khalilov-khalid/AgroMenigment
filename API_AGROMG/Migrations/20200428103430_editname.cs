using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class editname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfessions_Professions_ProfessionId",
                table: "UserProfessions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfessions_Workers_WorkersId",
                table: "UserProfessions");

            migrationBuilder.DropTable(
                name: "Techniques");

            migrationBuilder.DropTable(
                name: "TechniqueCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfessions",
                table: "UserProfessions");

            migrationBuilder.RenameTable(
                name: "UserProfessions",
                newName: "WorkerProfessions");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfessions_WorkersId",
                table: "WorkerProfessions",
                newName: "IX_WorkerProfessions_WorkersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfessions_ProfessionId",
                table: "WorkerProfessions",
                newName: "IX_WorkerProfessions_ProfessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkerProfessions",
                table: "WorkerProfessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProfessions_Professions_ProfessionId",
                table: "WorkerProfessions",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProfessions_Workers_WorkersId",
                table: "WorkerProfessions",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProfessions_Professions_ProfessionId",
                table: "WorkerProfessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProfessions_Workers_WorkersId",
                table: "WorkerProfessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkerProfessions",
                table: "WorkerProfessions");

            migrationBuilder.RenameTable(
                name: "WorkerProfessions",
                newName: "UserProfessions");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerProfessions_WorkersId",
                table: "UserProfessions",
                newName: "IX_UserProfessions_WorkersId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerProfessions_ProfessionId",
                table: "UserProfessions",
                newName: "IX_UserProfessions_ProfessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfessions",
                table: "UserProfessions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TechniqueCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsTrailer = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true)
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
                    Color = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    DateOfPurchase = table.Column<DateTime>(nullable: false),
                    EngineNumber = table.Column<string>(nullable: true),
                    EnginePower = table.Column<string>(nullable: true),
                    IsBusy = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductionYear = table.Column<DateTime>(nullable: false),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TechnicalCondition = table.Column<int>(nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfessions_Professions_ProfessionId",
                table: "UserProfessions",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfessions_Workers_WorkersId",
                table: "UserProfessions",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
