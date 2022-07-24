using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Migrations
{
    public partial class initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatedByUserID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_DeletedByUserID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UpdatedByUserID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_CreatedByUserID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_DeletedByUserID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Users_UpdatedByUserID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedByUserID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_DeletedByUserID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UpdatedByUserID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_CreatedByUserID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_DeletedByUserID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_UpdatedByUserID",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatedByUserID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_DeletedByUserID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UpdatedByUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatedByUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DeletedByUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UpdatedByUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CreatedByUserID",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_DeletedByUserID",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_UpdatedByUserID",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByUserID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedByUserID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpdatedByUserID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Phone_Number",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CreatedByUserID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DeletedByUserID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UpdatedByUserID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedByUserID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UpdatedByUserID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CreatedByUserID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedByUserID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserID",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                type: "nvarchar(max)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Phones",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CreatedByUserID",
                table: "Categories",
                column: "CreatedByUserID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CreatedByUserID",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Phones",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserID",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserID",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedByUserID",
                table: "Users",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeletedByUserID",
                table: "Users",
                column: "DeletedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedByUserID",
                table: "Users",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CreatedByUserID",
                table: "Purchases",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_DeletedByUserID",
                table: "Purchases",
                column: "DeletedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UpdatedByUserID",
                table: "Purchases",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserID",
                table: "Products",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedByUserID",
                table: "Products",
                column: "DeletedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedByUserID",
                table: "Products",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_Number",
                table: "Phones",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreatedByUserID",
                table: "Payments",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DeletedByUserID",
                table: "Payments",
                column: "DeletedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UpdatedByUserID",
                table: "Payments",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryID",
                table: "Categories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedByUserID",
                table: "Categories",
                column: "DeletedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedByUserID",
                table: "Categories",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryID",
                table: "Categories",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatedByUserID",
                table: "Categories",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_DeletedByUserID",
                table: "Categories",
                column: "DeletedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UpdatedByUserID",
                table: "Categories",
                column: "UpdatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_CreatedByUserID",
                table: "Payments",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_DeletedByUserID",
                table: "Payments",
                column: "DeletedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Users_UpdatedByUserID",
                table: "Payments",
                column: "UpdatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByUserID",
                table: "Products",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_DeletedByUserID",
                table: "Products",
                column: "DeletedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UpdatedByUserID",
                table: "Products",
                column: "UpdatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_CreatedByUserID",
                table: "Purchases",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_DeletedByUserID",
                table: "Purchases",
                column: "DeletedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_UpdatedByUserID",
                table: "Purchases",
                column: "UpdatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatedByUserID",
                table: "Users",
                column: "CreatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_DeletedByUserID",
                table: "Users",
                column: "DeletedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UpdatedByUserID",
                table: "Users",
                column: "UpdatedByUserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
