using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace E_commerce.ViewModel
{
    public class ProductsViewModel
    {
        public Product product { get; set; }
        public IList<IFormFile?> Files { get; set; }
        public List<Directorate> directorates { get; set; }
        public List<Governorate> governorates { get; set; }


    }
}
