using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class DetailsController : Controller
    {
        WebContext db;
        public DetailsController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string id)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");

            int productId = Convert.ToInt32(id);

            Product product = db.Products.FirstOrDefault(p => p.Id == productId);
            ViewBag.Product = product;

            List<Product> ProductList = db.Products.Where(pr => pr.NameAr.Trim().Contains(product.NameAr.Trim()) && pr.Id != product.Id).Take(10).ToList();
            ViewBag.ProductList = ProductList;

            return View();
        }

       
    }
}
