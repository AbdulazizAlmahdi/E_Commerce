using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class CheckController : Controller
    {
        public IActionResult Index(string phone)
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            ViewBag.phone = phone;

            return View();
        }
    }
}
