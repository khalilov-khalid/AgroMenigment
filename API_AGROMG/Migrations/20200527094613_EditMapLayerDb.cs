using Microsoft.EntityFrameworkCore.Migrations;

namespace API_AGROMG.Migrations
{
    public partial class EditMapLayerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapLayers_Companies_CompanyId",
                table: "MapLayers");

            migrationBuilder.DropForeignKey(
                name: "FK_MapLayers_Parcels_ParcelId",
                table: "MapLayers");

            migrationBuilder.AlterColumn<int>(
                name: "ParcelId",
                table: "MapLayers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "MapLayers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MapLayers_Companies_CompanyId",
                table: "MapLayers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapLayers_Parcels_ParcelId",
                table: "MapLayers",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapLayers_Companies_CompanyId",
                table: "MapLayers");

            migrationBuilder.DropForeignKey(
                name: "FK_MapLayers_Parcels_ParcelId",
                table: "MapLayers");

            migrationBuilder.AlterColumn<int>(
                name: "ParcelId",
                table: "MapLayers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "MapLayers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MapLayers_Companies_CompanyId",
                table: "MapLayers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MapLayers_Parcels_ParcelId",
                table: "MapLayers",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
