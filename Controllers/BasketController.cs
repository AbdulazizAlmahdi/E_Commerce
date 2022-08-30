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

            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            return View();
        }
    }
}
