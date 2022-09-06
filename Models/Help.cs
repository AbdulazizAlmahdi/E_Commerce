using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_commerce.Models
{
    public partial class Help
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "موضوع الرسالة مطلوب")]
        [StringLength(maximumLength: 100, ErrorMessage = "موضوع الرسالة لا يزيد عن 100 حرف")]
        [Display(Name = "موضوع الرسالة")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "نص الرسالة مطلوب")]
        [StringLength(maximumLength: 500, ErrorMessage = "نص الرسالة لا يزيد عن 500 حرف")]
        [Display(Name = "نص الرسالة")]
        public string Details { get; set; }
        public int? PhoneId { get; set; }
        [DefaultValue("Pending")]
        public string Status { get; set; }
        public virtual Phone Phone { get; set; }
         public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
