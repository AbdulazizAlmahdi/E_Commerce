using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class LoginController : Controller
    {
        WebContext db;
        public LoginController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string phone, [FromForm] string password)
        {
            ViewBag.PhoneOrg = phone;

            if (string.IsNullOrEmpty(phone?.Trim()))
            {
                ViewBag.Error = "يجب تعبئه حقل رقم الهاتف";

                return View();
            }
            foreach (char item in phone)
            {
                if (!char.IsDigit(item))
                {
                    ViewBag.Error = "رقم الهاتف غير صحيح، يجب ان يكون ارقام فقط";

                    return View();
                }
            }
            if (phone.Length != 9)
            {
                ViewBag.Error = "رقم الهاتف غير صحيح، يرجى التأكد من رقم الهاتف";

                return View();
            }

            Phone phoneRow = db.Phones.FirstOrDefault(ph => ph.Number == phone);

            if (phoneRow == null)
            {
                ViewBag.Error = "رقم الهاتف أو كلمة المرور غير صحيح";

                return View();
            }
            User user = db.Users.FirstOrDefault(i => i.PhoneId == phoneRow.Id);
            if (user == null || user.Password != password)
            {
                ViewBag.Error = "رقم الهاتف أو كلمة المرور غير صحيح";

                return View();
            }

            HttpContext.Session.SetString("phoneS", phone);
            HttpContext.Session.SetInt32("phoneId", phoneRow.Id);
            HttpContext.Session.SetString("userNameS", user.Name ?? "مستخدم");
            HttpContext.Session.SetInt32("idS", user.Id);
            HttpContext.Session.SetString("userAddress", user.Address ?? "لايوجد عنوان");

            ImagesUser imageRow = db.ImagesUsers.Where(img => img.UserId == user.Id).ToList().LastOrDefault();
            if (imageRow != null)
                HttpContext.Session.SetString("userImage", imageRow.ImageUrl);
            else
                HttpContext.Session.SetString("userImage", "users/default.png");

            return Redirect("/home");
        }
    }
}