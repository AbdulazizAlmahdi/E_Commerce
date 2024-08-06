using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_commerce.Models
{
    public partial class Auction
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "تاريخ بداء المزاد مطلوب")]
        [Display(Name = "تاريخ بداء المزاد")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "تاريخ نهاية المزاد مطلوب")]
        [Display(Name = "تاريخ نهاية المزاد")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "سعر المزاد مطلوب")]
        [Display(Name = "سعر المزاد")]
        [Range(0, 100000000, ErrorMessage = "السعر يجب أن يكون بين 0 و 100000000")]
        public decimal? StartPrice { get; set; }
        [Display(Name = "المنتج")]
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<AuctionsUser> AuctionsUsers { get; set; }
    }
}
