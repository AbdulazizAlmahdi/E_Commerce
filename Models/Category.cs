using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace E_commerce.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseCategoryNavigation = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? UpdatedByUserID { get; set; }
        public int? DeletedByUserID { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CreatedByUserID")]
        public virtual User CreatedByUser { get; set; }
        [ForeignKey("UpdatedByUserID")]
        public virtual User UpdatedByUser { get; set; }
        [ForeignKey("DeletedByUserID")]
        public virtual User DeletedByUser { get; set; }

        public virtual Category CategoryNavigation { get; set; }
        public virtual ICollection<Category> InverseCategoryNavigation { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
