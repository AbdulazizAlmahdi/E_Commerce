using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
        }

        public int Id { get; set; }
        public bool Status { get; set; }
        public decimal Amount { get; set; }
        public decimal ExtraAmount { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Detials { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }    
        public int UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual User User { get; set; }
    }
}
