using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace E_commerce.Models
{
    public class Governorate
    {
        public Governorate(int id, string name)
        {
            Id = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم المحافظة مطلوب")]
        [Display(Name = "اسم المحافظة")]
        // [Index(IsUnique = true)]
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Directorate> Directorates { get; set; }
    }
}
