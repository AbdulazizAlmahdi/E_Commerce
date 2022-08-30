using E_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class MainCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> subCategory { get; set; }
    }
}
