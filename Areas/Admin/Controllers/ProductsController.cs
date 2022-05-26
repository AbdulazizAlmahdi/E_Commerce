using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        IProductRepository<Product> products;

        public ProductsController(IProductRepository<Product> repository)
        {
            products = repository;
        }
        public IActionResult Index()
        {
            IList<Product> product = products.List();
            return View(product);
        }
    }
}
