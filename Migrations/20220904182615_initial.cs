using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", maxLength: 50, nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Places_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Roles_Permissions_PermissionsID",
                        column: x => x.PermissionsID,
                        principalTable: "Permissions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Helps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Helps_Phones_PhoneID",
                        column: x => x.PhoneID,
                        principalTable: "Phones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", maxLength: 50, nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneID = table.Column<int>(type: "int", nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Phones_PhoneID",
                        column: x => x.PhoneID,
                        principalTable: "Phones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagesUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImagesUser_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ExtraAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsers",
                columns: table => new
                {
                    RolesID = table.Column<int>(type: "int", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsers", x => new { x.RolesID, x.UsersID });
                    table.ForeignKey(
                        name: "FK_RolesUsers_Roles_RolesID",
                        column: x => x.RolesID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesUsers_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payments_Purchases_PurchaseID",
                        column: x => x.PurchaseID,
                        principalTable: "Purchases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DetailsAR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DetailsEN = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Views = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Evaluation = table.Column<int>(type: "int", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartPrice = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Auctions_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagesProduct",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesProduct", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImagesProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuctionsUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    AuctionID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionsUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuctionsUsers_Auctions_AuctionID",
                        column: x => x.AuctionID,
                        principalTable: "Auctions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionsUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ProductID",
                table: "Auctions",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionsUsers_AuctionID",
                table: "AuctionsUsers",
                column: "AuctionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionsUsers_AuctionID1",
                table: "AuctionsUsers",
                column: "AuctionID");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionsUsers_UserID",
                table: "AuctionsUsers",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionsUsers_UserID1",
                table: "AuctionsUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Categoreis_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductID",
                table: "Comments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Helps_PhoneID",
                table: "Helps",
                column: "PhoneID");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesProduct_ProductID",
                table: "ImagesProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ImagesUser_userID",
                table: "ImagesUser",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PurchaseID",
                table: "Payments",
                column: "PurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_PlaceID",
                table: "Places",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchaseId",
                table: "Products",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserID",
                table: "Purchases",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_PermissionsID",
                table: "Roles",
                column: "PermissionsID");

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsers_UsersID",
                table: "RolesUsers",
                column: "UsersID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneID",
                table: "Users",
                column: "PhoneID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlaceID",
                table: "Users",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UsersID",
                table: "Users",
                column: "UsersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionsUsers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Helps");

            migrationBuilder.DropTable(
                name: "ImagesProduct");

            migrationBuilder.DropTable(
                name: "ImagesUser");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RolesUsers");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
