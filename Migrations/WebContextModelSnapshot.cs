﻿// <auto-generated />
using System;
using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_commerce.Migrations
{
    [DbContext(typeof(WebContext))]
    partial class WebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E_commerce.Models.Auction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("StartPrice")
                        .IsRequired()
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "IX_Auctions_ProductID")
                        .IsUnique()
                        .HasFilter("[ProductID] IS NOT NULL");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("E_commerce.Models.AuctionsUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("AuctionId")
                        .HasColumnType("int")
                        .HasColumnName("AuctionID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex("AuctionId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserID] IS NOT NULL");

                    b.ToTable("AuctionsUsers");
                });

            modelBuilder.Entity("E_commerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Categoreis_CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("E_commerce.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "IX_Comments_ProductID");

                    b.HasIndex(new[] { "UserId" }, "IX_Comments_UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("E_commerce.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("E_commerce.Models.Directorate", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("GovernorateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("GovernorateId");

                    b.ToTable("Directorates");
                });

            modelBuilder.Entity("E_commerce.Models.Farmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DirectorateId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DirectorateId");

                    b.HasIndex("UserId");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("E_commerce.Models.Governorate", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("E_commerce.Models.Help", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhoneId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId");

                    b.ToTable("Helps");
                });

            modelBuilder.Entity("E_commerce.Models.ImagesProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "IX_ImagesProduct_ProductID");

                    b.ToTable("ImagesProduct");
                });

            modelBuilder.Entity("E_commerce.Models.ImagesUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_ImagesUser_userID");

                    b.ToTable("ImagesUser");
                });

            modelBuilder.Entity("E_commerce.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("E_commerce.Models.PaymentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created_at");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseId")
                        .HasColumnType("int")
                        .HasColumnName("PurchaseID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex(new[] { "PurchaseId" }, "IX_PaymentItems_PurchaseID");

                    b.ToTable("PaymentItems");
                });

            modelBuilder.Entity("E_commerce.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("E_commerce.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Number" }, "IX_Phone_Number")
                        .IsUnique();

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("E_commerce.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("int")
                        .HasColumnName("PlaceID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PlaceId" }, "IX_Places_PlaceID");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("E_commerce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DetailsAr")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("DetailsAR");

                    b.Property<string>("DetailsEn")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("DetailsEN");

                    b.Property<int?>("DirectorateId")
                        .HasColumnType("int");

                    b.Property<double?>("Discount")
                        .HasColumnType("float");

                    b.Property<int?>("Duration")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Evaluation")
                        .HasColumnType("int");

                    b.Property<int?>("FarmerId")
                        .HasColumnType("int");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NameAR");

                    b.Property<string>("NameEn")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NameEN");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("PurchaseId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Report")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DirectorateId");

                    b.HasIndex("FarmerId");

                    b.HasIndex("PurchaseId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Products_CategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("E_commerce.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(8,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created_at");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detials")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ExtraAmount")
                        .IsRequired()
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_Purchases_UserID");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("E_commerce.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PermissionsId")
                        .HasColumnType("int")
                        .HasColumnName("PermissionsID");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PermissionsId" }, "IX_Roles_PermissionsID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("E_commerce.Models.RolesUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int")
                        .HasColumnName("RolesID");

                    b.Property<int>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("UsersID");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex(new[] { "UsersId" }, "IX_RolesUsers_UsersID");

                    b.ToTable("RolesUsers");
                });

            modelBuilder.Entity("E_commerce.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("Created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DirectorateId")
                        .HasColumnType("int");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int")
                        .HasColumnName("PhoneID");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("int")
                        .HasColumnName("PlaceID");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("UsersID");

                    b.HasKey("Id");

                    b.HasIndex("DirectorateId");

                    b.HasIndex(new[] { "PhoneId" }, "IX_Users_PhoneID")
                        .IsUnique();

                    b.HasIndex(new[] { "PlaceId" }, "IX_Users_PlaceID");

                    b.HasIndex(new[] { "UsersId" }, "IX_Users_UsersID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("E_commerce.Models.Auction", b =>
                {
                    b.HasOne("E_commerce.Models.Product", "Product")
                        .WithOne("Auction")
                        .HasForeignKey("E_commerce.Models.Auction", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("E_commerce.Models.AuctionsUser", b =>
                {
                    b.HasOne("E_commerce.Models.Auction", "Auction")
                        .WithMany("AuctionsUsers")
                        .HasForeignKey("AuctionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("E_commerce.Models.User", "User")
                        .WithOne("AuctionsUser")
                        .HasForeignKey("E_commerce.Models.AuctionsUser", "UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Auction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Category", b =>
                {
                    b.HasOne("E_commerce.Models.Category", "categories")
                        .WithMany("InverseCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("E_commerce.Models.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("categories");

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Comment", b =>
                {
                    b.HasOne("E_commerce.Models.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("E_commerce.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Directorate", b =>
                {
                    b.HasOne("E_commerce.Models.Governorate", "Governorate")
                        .WithMany("Directorates")
                        .HasForeignKey("GovernorateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("E_commerce.Models.Farmer", b =>
                {
                    b.HasOne("E_commerce.Models.Directorate", "Directorate")
                        .WithMany("Farmers")
                        .HasForeignKey("DirectorateId");

                    b.HasOne("E_commerce.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Directorate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Help", b =>
                {
                    b.HasOne("E_commerce.Models.Phone", null)
                        .WithMany("Helps")
                        .HasForeignKey("PhoneId");
                });

            modelBuilder.Entity("E_commerce.Models.ImagesProduct", b =>
                {
                    b.HasOne("E_commerce.Models.Product", "Product")
                        .WithMany("ImagesProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("E_commerce.Models.ImagesUser", b =>
                {
                    b.HasOne("E_commerce.Models.User", "User")
                        .WithMany("ImagesUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Notification", b =>
                {
                    b.HasOne("E_commerce.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("E_commerce.Models.PaymentItem", b =>
                {
                    b.HasOne("E_commerce.Models.Product", "Product")
                        .WithMany("PaymentItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_commerce.Models.Purchase", "Purchase")
                        .WithMany("PaymentItems")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("E_commerce.Models.Place", b =>
                {
                    b.HasOne("E_commerce.Models.Place", "PlaceNavigation")
                        .WithMany("InversePlaceNavigation")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("PlaceNavigation");
                });

            modelBuilder.Entity("E_commerce.Models.Product", b =>
                {
                    b.HasOne("E_commerce.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("E_commerce.Models.Directorate", "Directorate")
                        .WithMany("Products")
                        .HasForeignKey("DirectorateId");

                    b.HasOne("E_commerce.Models.Farmer", "Farmer")
                        .WithMany("Products")
                        .HasForeignKey("FarmerId");

                    b.HasOne("E_commerce.Models.Purchase", "Purchase")
                        .WithMany("Products")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("E_commerce.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("Directorate");

                    b.Navigation("Farmer");

                    b.Navigation("Purchase");

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Purchase", b =>
                {
                    b.HasOne("E_commerce.Models.User", "User")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Role", b =>
                {
                    b.HasOne("E_commerce.Models.Permission", "Permissions")
                        .WithMany("Roles")
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("E_commerce.Models.RolesUser", b =>
                {
                    b.HasOne("E_commerce.Models.Role", "Roles")
                        .WithMany("RolesUsers")
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_commerce.Models.User", "Users")
                        .WithMany("RolesUsers")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("E_commerce.Models.User", b =>
                {
                    b.HasOne("E_commerce.Models.Directorate", "Directorate")
                        .WithMany("Users")
                        .HasForeignKey("DirectorateId");

                    b.HasOne("E_commerce.Models.Phone", "Phone")
                        .WithOne("User")
                        .HasForeignKey("E_commerce.Models.User", "PhoneId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("E_commerce.Models.Place", "Place")
                        .WithMany("Users")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("E_commerce.Models.User", "Users")
                        .WithMany("InverseUsers")
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Directorate");

                    b.Navigation("Phone");

                    b.Navigation("Place");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("E_commerce.Models.Auction", b =>
                {
                    b.Navigation("AuctionsUsers");
                });

            modelBuilder.Entity("E_commerce.Models.Category", b =>
                {
                    b.Navigation("InverseCategory");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("E_commerce.Models.Directorate", b =>
                {
                    b.Navigation("Farmers");

                    b.Navigation("Products");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("E_commerce.Models.Farmer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("E_commerce.Models.Governorate", b =>
                {
                    b.Navigation("Directorates");
                });

            modelBuilder.Entity("E_commerce.Models.Permission", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("E_commerce.Models.Phone", b =>
                {
                    b.Navigation("Helps");

                    b.Navigation("User");
                });

            modelBuilder.Entity("E_commerce.Models.Place", b =>
                {
                    b.Navigation("InversePlaceNavigation");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("E_commerce.Models.Product", b =>
                {
                    b.Navigation("Auction");

                    b.Navigation("Comments");

                    b.Navigation("ImagesProducts");

                    b.Navigation("PaymentItems");
                });

            modelBuilder.Entity("E_commerce.Models.Purchase", b =>
                {
                    b.Navigation("PaymentItems");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("E_commerce.Models.Role", b =>
                {
                    b.Navigation("RolesUsers");
                });

            modelBuilder.Entity("E_commerce.Models.User", b =>
                {
                    b.Navigation("AuctionsUser");

                    b.Navigation("Categories");

                    b.Navigation("Comments");

                    b.Navigation("ImagesUsers");

                    b.Navigation("InverseUsers");

                    b.Navigation("Purchases");

                    b.Navigation("RolesUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
