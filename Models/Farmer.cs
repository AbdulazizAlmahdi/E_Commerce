using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace E_commerce.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اسم المزارع")]
        public string Name { get; set; }
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "رقم الهاتف لايزيد او ينقص عن 9 ارقام")]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Display(Name = "عنوان المزارع")]
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [AllowNull]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("DirectorateId")]
        [AllowNull]
        public virtual int? DirectorateId { get; set; }
        [ForeignKey("UserId")]
        [AllowNull]
        public virtual int? UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual User User { get; set; }
        public virtual Directorate Directorate { get; set; }
    }
}
