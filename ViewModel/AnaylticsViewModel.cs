using E_commerce.Models;
using System.Collections.Generic;

namespace E_commerce.ViewModel
{
    public class AnaylticsViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<User> Users { get; set; }

        public Help Help { get; set; }

        public Purchase Purchase { get; set; }

        public Customer Customer { get; set; }

        public int AuctionCount { get; set; }

        public decimal PurchaseCount { get; set; }
        public int CatgoryCount { get; set; }
    }
}
