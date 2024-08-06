using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


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
        [Required(ErrorMessage = "المسمى الوظيفي مطلوب")]
        [StringLength(50, ErrorMessage = "المسمى الوظيفي لا يزيد عن 50 حرف")]
        [Display(Name = "المسمى الوظيفي")]
        [Column(TypeName = "nvarchar(max)")]
        public string JobName { get; set; }
        [Display(Name = "كلمة المرور")]
        [StringLength(maximumLength: 1000, MinimumLength = 6, ErrorMessage = "يجب ان لاتقل كلمة المرور على ستة حروف")]
        [Column(TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        [Required(ErrorMessage = "حالة المستخدم مطلوب")]
        [StringLength(50, ErrorMessage = "حالة المستخدم لا يزيد عن 50 حرف")]
        [Display(Name = "حالة المستخدم")]
        [Column(TypeName = "nvarchar(max)")]
        public string Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CreatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? UpdatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DeletedAt { get; set; }
        [ForeignKey("PhoneId")]
        public int PhoneId { get; set; }
        [ForeignKey("PlaceId")]
        public int? PlaceId { get; set; }
        [Display(Name = "المستخدم الاعلى")]
        [Column(TypeName = "int")]
        public int? UsersId { get; set; }
        [ForeignKey("DirectorateId")]
        [AllowNull]
        [Display(Name = " المديرية")]

        public virtual int? DirectorateId { get; set; }
        public virtual Phone Phone { get; set; }
        public virtual Place Place { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual User Users { get; set; }
        public virtual Directorate Directorate { get; set; }
        public virtual AuctionsUser AuctionsUser { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Purchase> Purchases { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Comment> Comments { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ImagesUser> ImagesUsers { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<User> InverseUsers { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<RolesUser> RolesUsers { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Category> Categories { get; set; }



    }
}
