using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toggler_Service.Migrations
{
    public partial class Remove_IsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ToggleServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Toggles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Services");

            migrationBuilder.CreateIndex(
                name: "IX_ToggleServices_ServiceId",
                table: "ToggleServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ToggleServices_ToggleId",
                table: "ToggleServices",
                column: "ToggleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToggleServices_Services_ServiceId",
                table: "ToggleServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToggleServices_Toggles_ToggleId",
                table: "ToggleServices",
                column: "ToggleId",
                principalTable: "Toggles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToggleServices_Services_ServiceId",
                table: "ToggleServices");

            migrationBuilder.DropForeignKey(
                name: "FK_ToggleServices_Toggles_ToggleId",
                table: "ToggleServices");

            migrationBuilder.DropIndex(
                name: "IX_ToggleServices_ServiceId",
                table: "ToggleServices");

            migrationBuilder.DropIndex(
                name: "IX_ToggleServices_ToggleId",
                table: "ToggleServices");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ToggleServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Toggles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
