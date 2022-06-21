using E_commerce.Migrations;
using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
   
    public class HomeController : Controller
    {

        WebContext db;
        public HomeController(WebContext db)
        {
            this.db = db;
        }

        public IActionResult Index([FromQuery]string search)
        {
            List<Product> products;

            if (search != null)
                products = db.Products.Where(prod =>
                prod.Status == true
                && (prod.NameAr.Trim().Contains(search.Trim())
                || prod.DetailsAr.Trim().Contains(search.Trim()))
                ).ToList();
            else
                products = db.Products.Where(prod => prod.Status == true).ToList();

            ViewBag.userS = HttpContext.Session.GetString("userNameS");

            return View(products);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userNameS");

            return Redirect("/home");
        }
        public IActionResult Privacy()
        {
            List<Help> help = db.Helps.ToList();
            List<Phone> users = db.Phones.ToList();

            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
