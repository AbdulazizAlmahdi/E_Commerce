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
    }
}
