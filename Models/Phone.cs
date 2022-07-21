using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual User User { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Help> Helps { get; set; }
    }
}
