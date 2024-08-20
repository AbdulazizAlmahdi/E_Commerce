using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace E_commerce.Migrations
{
    public partial class jd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Farmers",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Farmers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Farmers");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Farmers",
                newName: "CreateAt");
        }
    }
}
