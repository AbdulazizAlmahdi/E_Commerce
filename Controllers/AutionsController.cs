using E_commerce.Migrations;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_commerce.Controllers
{
    public class AutionsController : Controller
    {
        WebContext db;
        public AutionsController(WebContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");

            // var auctio = db.Auctions.ToList();

            var autionList = db.Auctions.Join( // first table 
                db.Products, //second table
                a => a.ProductId, // first table key
                p => p.Id, // second table key
                (a, p) => new { product = p, aution = a } // new data
                )
                .Where(ss => ss.aution.EndDate > DateTime.Now) // where condition
                .ToList();// converto to list, so we can use it in for

            List<AutionsProduct> autonProducts = new List<AutionsProduct>();

            foreach (var item in autionList)
            {
                autonProducts.Add(new AutionsProduct() { Auctions = item.aution, Products = item.product });
            }

            return View(autonProducts);
        }

        
    }
}
