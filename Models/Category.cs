using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#nullable disable

namespace E_commerce.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseCategory = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "الاسم مطلوب")]
        [StringLength(50, ErrorMessage = "الاسم لا يزيد عن 50 حرف")]
        [DisplayName("اسم الصنف")]
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category categories { get; set; }
        public virtual ICollection<Category> InverseCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
