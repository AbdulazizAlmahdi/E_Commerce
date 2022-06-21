using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");

            return View();
        }
    }
}
