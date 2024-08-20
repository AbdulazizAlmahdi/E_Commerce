using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
#nullable disable

namespace E_commerce.Models
{
    public partial class Phone
    {
        public Phone()
        {
            Helps = new HashSet<Help>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(maximumLength: 9, MinimumLength = 9, ErrorMessage = "رقم الهاتف لايزيد او ينقص عن 9 ارقام")]
        [Display(Name = "رقم الهاتف")]
        //[Index(IsUnique = true)]
        public string Number { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual User User { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Help> Helps { get; set; }
    }
}
