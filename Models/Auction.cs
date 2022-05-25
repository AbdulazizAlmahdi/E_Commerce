using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Auction
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartPrice { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual AuctionsUser AuctionsUser { get; set; }
    }
}
