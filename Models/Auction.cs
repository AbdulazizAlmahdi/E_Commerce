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
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "تاريخ نهاية المزاد مطلوب")]
        [Display(Name = "تاريخ نهاية المزاد")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "سعر المزاد مطلوب")]
        [Display(Name = "سعر المزاد")]
        public decimal StartPrice { get; set; }
        [Required(ErrorMessage = "المنتج مطلوب")]
        [Display(Name = "المنتج")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual AuctionsUser AuctionsUser { get; set; }
    }
}
