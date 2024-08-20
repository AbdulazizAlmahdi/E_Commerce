﻿#nullable disable

namespace E_commerce.Models
{
    public partial class AuctionsUser
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public virtual int? AuctionId { get; set; }
        public virtual int? UserId { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual User User { get; set; }
    }
}
