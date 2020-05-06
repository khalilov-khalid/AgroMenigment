using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class renameUserstoWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfessions_Users_UserId",
                table: "UserProfessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlanActionLogs_Users_PerformingUserId",
                table: "WorkPlanActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlanActionLogs_Users_ResponderId",
                table: "WorkPlanActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Users_CreatedId",
                table: "WorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Users_RespondentId",
                table: "WorkPlans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    AdminStatus = table.Column<bool>(nullable: false),
                    Adress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    PermissionsGroupsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workers_PermissionsGroups_PermissionsGroupsId",
                        column: x => x.PermissionsGroupsId,
                        principalTable: "PermissionsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workers_CompanyId",
                table: "Workers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PermissionsGroupsId",
                table: "Workers",
                column: "PermissionsGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfessions_Workers_UserId",
                table: "UserProfessions",
                column: "UserId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlanActionLogs_Workers_PerformingUserId",
                table: "WorkPlanActionLogs",
                column: "PerformingUserId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlanActionLogs_Workers_ResponderId",
                table: "WorkPlanActionLogs",
                column: "ResponderId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Workers_CreatedId",
                table: "WorkPlans",
                column: "CreatedId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_Workers_RespondentId",
                table: "WorkPlans",
                column: "RespondentId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfessions_Workers_UserId",
                table: "UserProfessions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlanActionLogs_Workers_PerformingUserId",
                table: "WorkPlanActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlanActionLogs_Workers_ResponderId",
                table: "WorkPlanActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Workers_CreatedId",
                table: "WorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_Workers_RespondentId",
                table: "WorkPlans");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminStatus = table.Column<bool>(nullable: false),
                    Adress = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PermissionsGroupsId = table.Column<int>(nullable: true),
                    Salary = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Tel = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_PermissionsGroups_PermissionsGroupsId",
                        column: x => x.PermissionsGroupsId,
                        principalTable: "PermissionsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyId",
                table: "Users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionsGroupsId",
                table: "Users",
                column: "PermissionsGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfessions_Users_UserId",
                table: "UserProfessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlanActionLogs_Users_PerformingUserId",
                table: "WorkPlanActionLogs",
                column: "PerformingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlanActionLogs_Users_ResponderId",
                table: "WorkPlanActionLogs",
                column: "ResponderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
