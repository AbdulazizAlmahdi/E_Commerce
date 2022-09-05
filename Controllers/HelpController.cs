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
    public class HelpController : Controller
    {
        WebContext db;
        public HelpController(WebContext db)
        {
            this.db = db;
        }

        private void initLayout()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");//Check Login
            ViewBag.PhoneOrg = HttpContext.Session.GetString("phoneS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;
        }

        public IActionResult Index()
        {
            initLayout();

            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]string phone, [FromForm] string subject, [FromForm] string details)
        {
            initLayout();

            ViewBag.PhoneOrg = phone;
            ViewBag.SubjectOrg = subject;
            ViewBag.DetailsOrg = details;

            if (string.IsNullOrEmpty(phone?.Trim()) || string.IsNullOrEmpty(subject?.Trim()) || string.IsNullOrEmpty(details?.Trim()))
            {
                if (phone == null)
                    ViewBag.Phone = "يجب تعبئه حقل رقم الهاتف";
                if (subject == null)
                    ViewBag.Subject = "يجب تعبئه حقل العنوان";
                if (details == null)
                    ViewBag.Details = "يجب تعبئه حقل الرساله";

                return View();
            }
            foreach (char item in phone)
            {
                if (!char.IsDigit(item))
                {
                    ViewBag.Phone = "رقم الهاتف غير صحيح، يجب ان يكون ارقام فقط";

                    return View();
                }
            }
            if (phone.Length != 9)
            {
                ViewBag.Phone = "رقم الهاتف غير صحيح، يرجى التأكد من رقم الهاتف";

                return View();
            }
            if (!phone.StartsWith("77") && !phone.StartsWith("73") && !phone.StartsWith("71") && !phone.StartsWith("70"))
            {
                ViewBag.Phone = "رقم الهاتف غير صحيح، يجب أن يكون رقم الهاتف يمني";

                return View();
            }

            Phone phoneRow = db.Phones.FirstOrDefault(ph => ph.Number == phone);

            if (phoneRow == null)
            {
                ViewBag.Phone = "رقم الهاتف غير مسجل،يرجى التسجيل اولاً";

                return View();
            }

            db.Helps.Add(new Help()
            {
                Phone = phoneRow,
                Subject = subject,
                Details = details,
                //Adding datatime for help
                CreatedAt = DateTime.Now,
            });

            db.SaveChanges();

            ViewBag.Success = "تمت طلب المساعده بنجاح";
            
            return View();
        }

    
    }
}
