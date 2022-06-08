using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace E_commerce.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            ImagesUsers = new HashSet<ImagesUser>();
            InverseUsers = new HashSet<User>();
            RolesUsers = new HashSet<RolesUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? UpdatedByUserID { get; set; }
        public int? DeletedByUserID { get; set; }
        public int UserStatusId { get; set; }
        public int PhoneId { get; set; }
        public int? PlaceId { get; set; }
        public int? UsersId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
        public virtual User DeletedByUser { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual Place Place { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual User Users { get; set; }
        public virtual AuctionsUser AuctionsUser { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ImagesUser> ImagesUsers { get; set; }
        public virtual ICollection<User> InverseUsers { get; set; }
        public virtual ICollection<User> CreatedByInverseUsers { get; set; }
        public virtual ICollection<User> UpdatedInverseUsers { get; set; }
        public virtual ICollection<User> DeletedInverseUsers { get; set; }
        public virtual ICollection<RolesUser> RolesUsers { get; set; }
        public virtual ICollection<Category> CreatedCategories { get; set; }
        public virtual ICollection<Category> UpdatedCategories { get; set; }
        public virtual ICollection<Category> DeletedCategories { get; set; }
        public virtual ICollection<Payment> CreatedPayments { get; set; }
        public virtual ICollection<Payment> UpdatedPayments { get; set; }
        public virtual ICollection<Payment> DeletedPayments { get; set; }
        public virtual ICollection<Product> CreatedProducts { get; set; }
        public virtual ICollection<Product> UpdatedProducts { get; set; }
        public virtual ICollection<Product> DeletedProducts { get; set; }
        public virtual ICollection<Purchase> CreatedPurchases { get; set; }
        public virtual ICollection<Purchase> UpdatedPurchases { get; set; }
        public virtual ICollection<Purchase> DeletedPurchases { get; set; }

    }
}
