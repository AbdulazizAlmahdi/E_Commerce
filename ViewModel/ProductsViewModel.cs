using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.ViewModel
{
    public class ProductsViewModel
    {
        public Product product {  get; set; }
        public IList<IFormFile> Files { get; set; }


    }
}
