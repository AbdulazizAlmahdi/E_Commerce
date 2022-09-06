using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace E_commerce.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "حالة الطلب مطلوب")]
        [Display(Name = "حالة الطلب")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "إجمالي الطلب مطلوب")]
        [Display(Name = "إجمالي الطلب")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "المبلغ الاضافي مطلوب")]
        [Display(Name = "مبالغ اضافية")]
        [Range(minimum: 0.0, maximum: 1000000, ErrorMessage = "المبلغ الاضافي يجب أن يكون بين 0.0 و 1000000")]
        [DefaultValue(0.00)]
        public decimal? ExtraAmount { get; set; }
        [Required(ErrorMessage = "العنوان مطلوب")]
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(maximumLength: 9,MinimumLength =9, ErrorMessage = "رقم الهاتف لايزيد او ينقص عن 9 ارقام")]
        [Display(Name = "رقم الهاتف")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "التفاصيل مطلوب")]
        [Display(Name = "التفاصيل")]
        public string Detials { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }    
        public int UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual User User { get; set; }
    }
}
