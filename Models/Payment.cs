using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int? CreatedByUserID { get; set; }
        public int? UpdatedByUserID { get; set; }
        public int? DeletedByUserID { get; set; }
        public int? PurchaseId { get; set; }
        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
        public virtual User DeletedByUser { get; set; }

        public virtual Purchase Purchase { get; set; }
    }
}
