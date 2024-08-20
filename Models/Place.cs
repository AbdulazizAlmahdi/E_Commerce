using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
#nullable disable

namespace E_commerce.Models
{
    public partial class Place
    {
        public Place()
        {
            InversePlaceNavigation = new HashSet<Place>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "اسم المنطقة مطلوب")]
        [StringLength(50, ErrorMessage = "اسم المنطقة لا يزيد عن 50 حرف")]
        [Display(Name = "اسم المنطقة")]
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        public int? PlaceId { get; set; }

        public virtual Place PlaceNavigation { get; set; }
        public virtual ICollection<Place> InversePlaceNavigation { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<User> Users { get; set; }

    }
}
