using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Migrations
{
    public partial class notifications2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Purchases_purchaseId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_purchaseId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "purchaseId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "purchaseId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_purchaseId",
                table: "Notifications",
                column: "purchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Purchases_purchaseId",
                table: "Notifications",
                column: "purchaseId",
                principalTable: "Purchases",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
