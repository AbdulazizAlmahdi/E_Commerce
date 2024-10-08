﻿using Microsoft.EntityFrameworkCore;

#nullable disable

namespace E_commerce.Models
{
    public partial class WebContext : DbContext
    {
        public WebContext()
        {
        }

        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<AuctionsUser> AuctionsUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Help> Helps { get; set; }
        public virtual DbSet<ImagesProduct> ImagesProducts { get; set; }
        public virtual DbSet<ImagesUser> ImagesUsers { get; set; }
        public virtual DbSet<PaymentItem> PaymentItems { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesUser> RolesUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Directorate> Directorates { get; set; }
        public virtual DbSet<Governorate> Governorates { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=e_commerce;Trusted_Connection=True;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auction>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Auctions_ProductID")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StartPrice).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Auction)
                    .HasForeignKey<Auction>(d => d.ProductId).OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(a => a.AuctionsUsers)
                .WithOne(a => a.Auction)
                .HasForeignKey(a => a.AuctionId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<AuctionsUser>(entity =>
            {
                /*                entity.HasIndex(e => e.AuctionId, "IX_AuctionsUsers_AuctionID");

                                entity.HasIndex(e => e.UserId, "IX_AuctionsUsers_UserID");*/

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.AuctionId).HasColumnName("AuctionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(a => a.Auction)
                    .WithMany(u => u.AuctionsUsers).OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AuctionsUser)
                    .HasForeignKey<AuctionsUser>(d => d.UserId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Category>(entity =>
            {

                entity.HasIndex(e => e.CategoryId, "IX_Categoreis_CategoryId");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");

                entity.HasOne(d => d.categories)
                    .WithMany(p => p.InverseCategory)
                    .HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(c => c.User)
                    .WithMany(u => u.Categories)
                    .HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.SetNull);


            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Comments_ProductID");

                entity.HasIndex(e => e.UserId, "IX_Comments_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Help>(entity =>
            {
                //entity.HasIndex(e => e.PhoneId, "IX_Helps_PhoneID");

                entity.Property(e => e.Id).HasColumnName("ID");

                //entity.Property(e => e.PhoneId).HasColumnName("PhoneID");

                /*entity.HasOne(d => d.Phone)
                    .WithMany(p => p.Helps)
                    .HasForeignKey(d => d.PhoneId).OnDelete(DeleteBehavior.Cascade);*/
            });

            modelBuilder.Entity<ImagesProduct>(entity =>
            {
                entity.ToTable("ImagesProduct");

                entity.HasIndex(e => e.ProductId, "IX_ImagesProduct_ProductID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ImagesProducts)
                    .HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ImagesUser>(entity =>
            {
                entity.ToTable("ImagesUser");

                entity.HasIndex(e => e.UserId, "IX_ImagesUser_userID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ImagesUsers)
                    .HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PaymentItem>(entity =>
            {
                entity.HasIndex(e => e.PurchaseId, "IX_PaymentItems_PurchaseID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");



            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                //TODO unique phone number
                entity.HasIndex(e => e.Number, "IX_Phone_Number")
                    .IsUnique();
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasIndex(e => e.PlaceId, "IX_Places_PlaceID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

                entity.HasOne(d => d.PlaceNavigation)
                    .WithMany(p => p.InversePlaceNavigation)
                    .HasForeignKey(d => d.PlaceId).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryID");

                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.DetailsAr).HasColumnName("DetailsAR");

                entity.Property(e => e.DetailsEn).HasColumnName("DetailsEN");

                entity.Property(e => e.NameAr).HasColumnName("NameAR");

                entity.Property(e => e.NameEn).HasColumnName("NameEN");

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PurchaseId).OnDelete(DeleteBehavior.SetNull);




            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Purchases_UserID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.ExtraAmount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasMany(d => d.Products)
                    .WithOne(p => p.Purchase).OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.SetNull);



            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.PermissionsId, "IX_Roles_PermissionsID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PermissionsId).HasColumnName("PermissionsID");

                entity.HasOne(d => d.Permissions)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.PermissionsId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<RolesUser>(entity =>
            {
                entity.HasKey(e => new { e.RolesId, e.UsersId });

                entity.HasIndex(e => e.UsersId, "IX_RolesUsers_UsersID");

                entity.Property(e => e.RolesId).HasColumnName("RolesID");

                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.RolesUsers)
                    .HasForeignKey(d => d.RolesId);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.RolesUsers)
                    .HasForeignKey(d => d.UsersId);
            });



            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.PhoneId, "IX_Users_PhoneID")
                    .IsUnique();

                entity.HasIndex(e => e.PlaceId, "IX_Users_PlaceID");

                entity.HasIndex(e => e.UsersId, "IX_Users_UsersID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.PhoneId).HasColumnName("PhoneID");

                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");


                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.HasOne(d => d.Phone)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.PhoneId).OnDelete(DeleteBehavior.ClientCascade);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PlaceId).OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.InverseUsers)
                    .HasForeignKey(d => d.UsersId).OnDelete(DeleteBehavior.NoAction);


            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
