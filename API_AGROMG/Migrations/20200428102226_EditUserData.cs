using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_PermissionsGroups_PermissionsGroupsId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_PermissionsGroupsId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PermissionsGroupsId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Workers",
                newName: "WorkEndReason");

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkEndDate",
                table: "Workers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkStartDate",
                table: "Workers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkStatus",
                table: "Workers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserProfessions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Startdate",
                table: "UserProfessions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "UserProfessions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkersId = table.Column<int>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PermissionsGroupsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_PermissionsGroups_PermissionsGroupsId",
                        column: x => x.PermissionsGroupsId,
                        principalTable: "PermissionsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Workers_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkersId = table.Column<int>(nullable: true),
                    NetSalary = table.Column<decimal>(nullable: false),
                    GrossSalary = table.Column<decimal>(nullable: false),
                    StartSalary = table.Column<DateTime>(nullable: false),
                    EndSalary = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSalaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerSalaries_Workers_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionsGroupsId",
                table: "Users",
                column: "PermissionsGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkersId",
                table: "Users",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSalaries_WorkersId",
                table: "WorkerSalaries",
                column: "WorkersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkerSalaries");

            migrationBuilder.DropColumn(
                name: "WorkEndDate",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkStartDate",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserProfessions");

            migrationBuilder.DropColumn(
                name: "Startdate",
                table: "UserProfessions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserProfessions");

            migrationBuilder.RenameColumn(
                name: "WorkEndReason",
                table: "Workers",
                newName: "Username");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionsGroupsId",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Workers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PermissionsGroupsId",
                table: "Workers",
                column: "PermissionsGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_PermissionsGroups_PermissionsGroupsId",
                table: "Workers",
                column: "PermissionsGroupsId",
                principalTable: "PermissionsGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
