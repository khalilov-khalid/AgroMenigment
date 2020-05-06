using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditINAndOut2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemporaryExsel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    TemporaryOperationKindId = table.Column<int>(nullable: true),
                    TemporaryInAndOutItemsId = table.Column<int>(nullable: true),
                    TemporaryCustomerId = table.Column<int>(nullable: true),
                    TemporaryAccountKindId = table.Column<int>(nullable: true),
                    TemporaryPayAccountId = table.Column<int>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    User = table.Column<string>(nullable: true),
                    TemporaryParcelId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryExsel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryExsel_TemporaryAccountKind_TemporaryAccountKindId",
                        column: x => x.TemporaryAccountKindId,
                        principalTable: "TemporaryAccountKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryExsel_TemporaryCustomer_TemporaryCustomerId",
                        column: x => x.TemporaryCustomerId,
                        principalTable: "TemporaryCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryExsel_TemporaryInAndOutItems_TemporaryInAndOutItemsId",
                        column: x => x.TemporaryInAndOutItemsId,
                        principalTable: "TemporaryInAndOutItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryExsel_TemporaryOperationKind_TemporaryOperationKindId",
                        column: x => x.TemporaryOperationKindId,
                        principalTable: "TemporaryOperationKind",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryExsel_TemporaryParcel_TemporaryParcelId",
                        column: x => x.TemporaryParcelId,
                        principalTable: "TemporaryParcel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporaryExsel_TemporaryPayAccount_TemporaryPayAccountId",
                        column: x => x.TemporaryPayAccountId,
                        principalTable: "TemporaryPayAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryExsel_TemporaryAccountKindId",
                table: "TemporaryExsel",
                column: "TemporaryAccountKindId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryExsel_TemporaryCustomerId",
                table: "TemporaryExsel",
                column: "TemporaryCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryExsel_TemporaryInAndOutItemsId",
                table: "TemporaryExsel",
                column: "TemporaryInAndOutItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryExsel_TemporaryOperationKindId",
                table: "TemporaryExsel",
                column: "TemporaryOperationKindId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryExsel_TemporaryParcelId",
                table: "TemporaryExsel",
                column: "TemporaryParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryExsel_TemporaryPayAccountId",
                table: "TemporaryExsel",
                column: "TemporaryPayAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemporaryExsel");
        }
    }
}
