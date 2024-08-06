using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace E_commerce.Migrations
{
    public partial class editfarmer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farmer_Directorates_DirectorateId",
                table: "Farmer");

            migrationBuilder.DropForeignKey(
                name: "FK_Farmer_Users_UserId",
                table: "Farmer");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Farmer_FarmerId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Farmer",
                table: "Farmer");

            migrationBuilder.RenameTable(
                name: "Farmer",
                newName: "Farmers");

            migrationBuilder.RenameIndex(
                name: "IX_Farmer_UserId",
                table: "Farmers",
                newName: "IX_Farmers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Farmer_DirectorateId",
                table: "Farmers",
                newName: "IX_Farmers_DirectorateId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Farmers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Farmers",
                table: "Farmers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Farmers_Directorates_DirectorateId",
                table: "Farmers",
                column: "DirectorateId",
                principalTable: "Directorates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Farmers_Users_UserId",
                table: "Farmers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Farmers_FarmerId",
                table: "Products",
                column: "FarmerId",
                principalTable: "Farmers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Farmers_Directorates_DirectorateId",
                table: "Farmers");

            migrationBuilder.DropForeignKey(
                name: "FK_Farmers_Users_UserId",
                table: "Farmers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Farmers_FarmerId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Farmers",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Farmers");

            migrationBuilder.RenameTable(
                name: "Farmers",
                newName: "Farmer");

            migrationBuilder.RenameIndex(
                name: "IX_Farmers_UserId",
                table: "Farmer",
                newName: "IX_Farmer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Farmers_DirectorateId",
                table: "Farmer",
                newName: "IX_Farmer_DirectorateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Farmer",
                table: "Farmer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Farmer_Directorates_DirectorateId",
                table: "Farmer",
                column: "DirectorateId",
                principalTable: "Directorates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Farmer_Users_UserId",
                table: "Farmer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Farmer_FarmerId",
                table: "Products",
                column: "FarmerId",
                principalTable: "Farmer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
