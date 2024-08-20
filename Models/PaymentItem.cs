using System;

#nullable disable

namespace E_commerce.Models
{
    public partial class PaymentItem
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get { return Amount * Quantity; } set { _ = this.Amount * this.Quantity; } }
        // public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public virtual int PurchaseId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual Product Product { get; set; }
    }
}
