using E_commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.ViewModel
{
    public class PurchaseViewModel
    {
        public Purchase purchase {  get; set; }
        public List<Product> products { get; set; }

    }
}
