using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace E_commerce.Models
{
    public partial class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            ImagesProducts = new HashSet<ImagesProduct>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [Display(Name = "اسم المنتج")]
        [StringLength(maximumLength: 100, ErrorMessage = "اسم المنتج لا يزيد عن 100 حرف")]
        public string NameAr { get; set; }
        [Display(Name = "اسم المنتج")]
        [StringLength(maximumLength: 100, ErrorMessage = "اسم المنتج لا يزيد عن 100 حرف")]
        [AllowNull]
        public string NameEn { get; set; }
        [Display(Name = "تفاصيل المنتج")]
        [StringLength(maximumLength: 500, ErrorMessage = "تفاصيل المنتج لا يزيد عن 500 حرف")]
        public string DetailsAr { get; set; }
        [Display(Name = "تفاصيل المنتج")]
        [StringLength(maximumLength: 500, ErrorMessage = "تفاصيل المنتج لا يزيد عن 500 حرف")]
        [AllowNull]
        public string DetailsEn { get; set; }
        [Display(Name = "عنوان المنتج")]
        [StringLength(maximumLength: 100, ErrorMessage = "عنوان المنتج لا يزيد عن 100 حرف")]
        [Required(ErrorMessage = "عنوان المنتج مطلوب")]
        public string Address { get; set; }
        [AllowNull]
        public string Report { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Display(Name = "سعر المنتج")]
        [Required(ErrorMessage = "سعر المنتج مطلوب")]
        public decimal Price { get; set; }
        [Display(Name = "المدة")]
        [Required(ErrorMessage = "المدة مطلوبة")]
        public int Duration { get; set; }
        [Display(Name = "الحالة")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "الكمية مطلوبة")]
        [Display(Name = "الكمية")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "الكمية لا يمكن ان تكون 0")]
        public int Quantity { get; set; }
        [Display(Name = "الوحدة")]
        [AllowNull]
        public string Unit { get; set; }
        [AllowNull]
        public int? Views { get; set; }
        [Display(Name = "الخصم")]
        [AllowNull]
        public int? Discount { get; set; }
        [AllowNull]
        public int? UserId { get; set; }
        [AllowNull]
        public int? Evaluation { get; set; }
        public DateTime CreatedAt { get; set; }
        [AllowNull]
        public DateTime? UpdatedAt { get; set; }
        [AllowNull]
        public DateTime? DeletedAt { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("PurchaseId")]
        public int? PurchaseId { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual Auction Auction { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ImagesProduct> ImagesProducts { get; set; }
    }
}
