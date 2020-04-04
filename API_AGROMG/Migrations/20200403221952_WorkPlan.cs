using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class WorkPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FinisDate = table.Column<DateTime>(nullable: false),
                    ActionId = table.Column<int>(nullable: true),
                    RespondentIdId = table.Column<int>(nullable: true),
                    CreatedIdId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlans_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_Users_CreatedIdId",
                        column: x => x.CreatedIdId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_Users_RespondentIdId",
                        column: x => x.RespondentIdId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_ActionId",
                table: "WorkPlans",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_CompanyId",
                table: "WorkPlans",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_CreatedIdId",
                table: "WorkPlans",
                column: "CreatedIdId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_RespondentIdId",
                table: "WorkPlans",
                column: "RespondentIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkPlans");

            migrationBuilder.DropTable(
                name: "Actions");
        }
    }
}
