using System;
using System.Collections.Generic;

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
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Help> Helps { get; set; }
    }
}
