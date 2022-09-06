using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class RegisterController : Controller
    {
        WebContext db;
        public RegisterController(WebContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            return View();
        }
       

        [HttpPost]
        public IActionResult Index([FromForm] string name, [FromForm] string address, [FromForm] string password,[FromForm] string password2 ,[FromForm] string phone)
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            ViewBag.NameOrg = name;
            ViewBag.AddressOrg = address;
            ViewBag.PhoneOrg = phone;

            if (string.IsNullOrEmpty(phone?.Trim()))
            {
                ViewBag.Error = "يجب تعبئه حقل رقم الهاتف";

                return View();
            }

            /* التحقق من أن رقم الهاتف ارقام فقط */
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
            if (!phone.StartsWith("77") && !phone.StartsWith("73") && !phone.StartsWith("71") && !phone.StartsWith("70"))
            {
                ViewBag.Error = "رقم الهاتف غير صحيح، يجب أن يكون رقم الهاتف يمني";

                return View();
            }

            /** ********************* **/
            Phone phoneRow = db.Phones.FirstOrDefault(ph => ph.Number == phone);

            if (phoneRow != null)
            {
                ViewBag.Error = "الرقم موجود مسبقاً";

                return View();
            }

            /** ********************* **/
            db.Phones.Add(new Phone()
            {
                Number = phone,
                CreatedAt = DateTime.Now,
            });
            db.SaveChanges();

            /** ********************* **/
            phoneRow = db.Phones.FirstOrDefault(ph => ph.Number == phone);
            if (phoneRow == null)
            {
                ViewBag.Error = "حدثت مشكلة يرجى التواصل مع الدعم الفني";
                return View();
            }

            if (password == null)
            {
                password = "123456";
                password2 = password;
            }
            else if (password != password2)
            {
                ViewBag.Error = "كلمةالمرور غير متساويه";
                return View();
            }
            name = name ?? ("User " + phone);
            address = address ?? "اليمن";

            db.Users.Add(new User()
            {
                Phone = phoneRow,
                Name = name,
                Address = address,
                Password = password,
                CreatedAt = DateTime.Now,
                UsersId = null,
                JobName="عميل",
                Status="موقف",
            });

            db.SaveChanges();
            
            ViewBag.success = true;

            return View();
        }
    }
}
