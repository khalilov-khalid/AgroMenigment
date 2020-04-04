using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class WorkPanActionlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Users_CreatedIdId",
                table: "WorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Users_RespondentIdId",
                table: "WorkPlans");

            migrationBuilder.RenameColumn(
                name: "RespondentIdId",
                table: "WorkPlans",
                newName: "RespondentId");

            migrationBuilder.RenameColumn(
                name: "CreatedIdId",
                table: "WorkPlans",
                newName: "CreatedId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPlans_RespondentIdId",
                table: "WorkPlans",
                newName: "IX_WorkPlans_RespondentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPlans_CreatedIdId",
                table: "WorkPlans",
                newName: "IX_WorkPlans_CreatedId");

            migrationBuilder.CreateTable(
                name: "WorkPlanActionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkPlanId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Finishdate = table.Column<DateTime>(nullable: false),
                    ResponderId = table.Column<int>(nullable: true),
                    ActionId = table.Column<int>(nullable: true),
                    ActionTime = table.Column<DateTime>(nullable: false),
                    PerformingUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlanActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlanActionLogs_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanActionLogs_Users_PerformingUserId",
                        column: x => x.PerformingUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanActionLogs_Users_ResponderId",
                        column: x => x.ResponderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanActionLogs_WorkPlans_WorkPlanId",
                        column: x => x.WorkPlanId,
                        principalTable: "WorkPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlanTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkPlanId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlanTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlanTasks_WorkPlans_WorkPlanId",
                        column: x => x.WorkPlanId,
                        principalTable: "WorkPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlanTaskActionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkPlanActionLogId = table.Column<int>(nullable: true),
                    WorkPlanTaskId = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    Startdate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    ActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlanTaskActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkPlanTaskActionLogs_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanTaskActionLogs_WorkPlanActionLogs_WorkPlanActionLogId",
                        column: x => x.WorkPlanActionLogId,
                        principalTable: "WorkPlanActionLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlanTaskActionLogs_WorkPlanTasks_WorkPlanTaskId",
                        column: x => x.WorkPlanTaskId,
                        principalTable: "WorkPlanTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanActionLogs_ActionId",
                table: "WorkPlanActionLogs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanActionLogs_PerformingUserId",
                table: "WorkPlanActionLogs",
                column: "PerformingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanActionLogs_ResponderId",
                table: "WorkPlanActionLogs",
                column: "ResponderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanActionLogs_WorkPlanId",
                table: "WorkPlanActionLogs",
                column: "WorkPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTaskActionLogs_ActionId",
                table: "WorkPlanTaskActionLogs",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTaskActionLogs_WorkPlanActionLogId",
                table: "WorkPlanTaskActionLogs",
                column: "WorkPlanActionLogId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTaskActionLogs_WorkPlanTaskId",
                table: "WorkPlanTaskActionLogs",
                column: "WorkPlanTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlanTasks_WorkPlanId",
                table: "WorkPlanTasks",
                column: "WorkPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Users_CreatedId",
                table: "WorkPlans",
                column: "CreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Users_RespondentId",
                table: "WorkPlans",
                column: "RespondentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Users_CreatedId",
                table: "WorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Users_RespondentId",
                table: "WorkPlans");

            migrationBuilder.DropTable(
                name: "WorkPlanTaskActionLogs");

            migrationBuilder.DropTable(
                name: "WorkPlanActionLogs");

            migrationBuilder.DropTable(
                name: "WorkPlanTasks");

            migrationBuilder.RenameColumn(
                name: "RespondentId",
                table: "WorkPlans",
                newName: "RespondentIdId");

            migrationBuilder.RenameColumn(
                name: "CreatedId",
                table: "WorkPlans",
                newName: "CreatedIdId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPlans_RespondentId",
                table: "WorkPlans",
                newName: "IX_WorkPlans_RespondentIdId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkPlans_CreatedId",
                table: "WorkPlans",
                newName: "IX_WorkPlans_CreatedIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Users_CreatedIdId",
                table: "WorkPlans",
                column: "CreatedIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Users_RespondentIdId",
                table: "WorkPlans",
                column: "RespondentIdId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
