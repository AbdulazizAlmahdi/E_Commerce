﻿using E_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_app.Models;

namespace E_commerce.ViewModel
{
    public class AnaylticsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        
        public IEnumerable<User> Users {get; set;}

        public Help Help { get; set; }

        public Purchase Purchase { get; set; }

        public Customer Customer { get; set; }

        public int AuctionCount { get; set; }

        public decimal PurchaseCount { get; set; }
    }
}
