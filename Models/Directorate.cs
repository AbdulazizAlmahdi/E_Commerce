using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Directorate
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "اسم المديرية")]
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public virtual int GovernorateId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Farmer> Farmers { get; set; }
        public virtual Governorate Governorate { get; set; }


    }
}
