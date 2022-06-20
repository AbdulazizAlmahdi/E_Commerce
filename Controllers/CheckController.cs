using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class CheckController : Controller
    {
        public IActionResult Index(string phone)
        {
            ViewBag.phone = phone;

            return View();
        }
    }
}
