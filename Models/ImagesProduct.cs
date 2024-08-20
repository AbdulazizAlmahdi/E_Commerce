using System.ComponentModel.DataAnnotations;

#nullable disable

namespace E_commerce.Models
{
    public partial class ImagesProduct
    {
        public int Id { get; set; }
        [Display(Name = "الصور")]
        [Required(ErrorMessage = "الصور مطلوبة")]
        public string ImageUrl { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
