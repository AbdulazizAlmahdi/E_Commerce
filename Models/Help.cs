using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_commerce.Models
{
    public partial class Help
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "موضوع الرسالة")]
        public string Subject { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "نص الرسالة")]
        public string Details { get; set; }
        public int? PhoneId { get; set; }

        public virtual Phone Phone { get; set; }
         public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
