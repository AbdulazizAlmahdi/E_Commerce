using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public bool Status { get; set; }
        public decimal Amount { get; set; }
        public decimal ExtraAmount { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    
        public int UserId { get; set; }
        public int ProductId { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
