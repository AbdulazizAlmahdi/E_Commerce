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
    public class DetailsAutionController : Controller
    {
        WebContext db;
        public DetailsAutionController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string id)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");

            int auctionId = Convert.ToInt32(id);


            var data = db.Auctions.Join( // first table 
               db.Products, //second table
               a => a.ProductId, // first table key
               p => p.Id, // second table key
               (a, p) => new { product = p, aution = a } // new data
               )
               .FirstOrDefault(ss => ss.aution.Id == auctionId); // where condition

            ViewBag.Auction = new AutionsProduct() { Auctions = data.aution, Products = data.product };

            return View();
        }
    }
}
