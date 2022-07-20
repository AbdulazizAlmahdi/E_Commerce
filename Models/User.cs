using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


#nullable disable

namespace E_commerce.Models
{
    [Table("Users")]
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            ImagesUsers = new HashSet<ImagesUser>();
            InverseUsers = new HashSet<User>();
            RolesUsers = new HashSet<RolesUser>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم لا يزيد عن 50 حرف")]
        [DisplayName("الاسم")]
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        [Required(ErrorMessage = "العنوان مطلوب")]
        [StringLength(50, ErrorMessage = "العنوان لا يزيد عن 50 حرف")]
        [Display(Name = "العنوان")]
        [Column(TypeName = "nvarchar(max)")]        
        public string Address { get; set; }
        [Display(Name = "كلمة المرور")]
        [Column(TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? UpdatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DeletedAt { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? UpdatedByUserID { get; set; }
        public int? DeletedByUserID { get; set; }
        public int UserStatusId { get; set; }
        public int PhoneId { get; set; }
        public int? PlaceId { get; set; }
        [Display(Name = "المستخدم الاعلى")]
        [Column(TypeName = "int")]
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
