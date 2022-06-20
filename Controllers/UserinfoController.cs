using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class UserinfoController : Controller
    {
        WebContext db;
        public UserinfoController(WebContext db)
        {
            this.db = db;
        }
        private void initLayout()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.phones = HttpContext.Session.GetString("phoneS");
            ViewBag.userAddress = HttpContext.Session.GetString("userAddress");
        }
        public IActionResult Index()
        {
            initLayout();
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            return View();
        }
        
        [HttpPost]
        public IActionResult index([FromForm]string newName, [FromForm]string newAddress)
        {
            initLayout();
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            if (string.IsNullOrEmpty(newName?.Trim()) || string.IsNullOrEmpty(newAddress?.Trim()))
            {
                if (newName == null)
                    ViewBag.ErrorNewPass = "من فضلك قم بكتابة اسم المستخدم";
                if (newAddress == null)
                    ViewBag.ErrorNewPass2 = "من فضلك قم بكتابة العنوان";

                return View();
            }

            int? userId = HttpContext.Session.GetInt32("idS");

            User user = db.Users.FirstOrDefault(u => u.Id == userId);
            
            if (user == null)
            {
                ViewBag.Error = "من فضلك قم بالتواصل بالدعم الفني";
                return View();
            }


            user.Name = newName;
            user.Address = newAddress;
            db.SaveChanges();

            HttpContext.Session.SetString("userNameS", user.Name ?? "مستخدم");
            HttpContext.Session.SetString("userAddress", user.Address ?? "لايوجد عنوان");
            initLayout();

            return View();
        }

    }
}
