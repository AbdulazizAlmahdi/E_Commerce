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
    public class BasketController : Controller
    {
        WebContext db;
        public BasketController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;
            ViewBag.cart = Cart.getInstance();

            return View();
        }

        [HttpPost]
        public IActionResult Increment(int id)
        {
            Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
            if(cart != null)
                cart.Quantity++;
            return Redirect("/Basket");
        }

        [HttpPost]
        public IActionResult Decrement(int id)
        {
            Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
            if (cart != null)
                cart.Quantity--;
            return Redirect("/Basket");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
            if (cart != null)
                Cart.getInstance().Remove(cart);
            return Redirect("/Basket");
        }

        [HttpPost]
        public IActionResult Done([FromForm] String userAddress, [FromForm]String detials)
        {
            String phone = HttpContext.Session.GetString("phoneS");
            int userId = HttpContext.Session.GetInt32("idS") ?? 0;

            userAddress = userAddress ?? HttpContext.Session.GetString("userAddress");
            detials = detials ?? "";

            foreach (var cart in Cart.getInstance())
            {
                Purchase purchase = new Purchase()
                {
                    Amount = (cart.Quantity * ((decimal)cart.Price)),
                    ExtraAmount = 3000,
                    Phone = phone,
                    Address = userAddress,
                    Detials = detials,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    DeletedAt = DateTime.Now,
                    UserId = userId,
                };
                db.Purchases.Add(purchase);
                db.SaveChanges();
                Product product = db.Products.FirstOrDefault(p => p.Id == cart.Id);
                if (product != null)
                {
                    product.PurchaseId = purchase.Id;
                }

                db.SaveChanges();
            }
            

            Cart.getInstance().Clear();
            return Redirect("/Basket");
        }
    }
}
