using E_commerce.Models;
using System.Collections.Generic;

namespace E_commerce.Controllers
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> subCategory { get; set; }
    }
}
