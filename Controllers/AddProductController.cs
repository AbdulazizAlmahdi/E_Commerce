using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class AddProductController : Controller
    {
        WebContext db;
        public AddProductController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm] string productName, [FromForm] string price, [FromForm] string quantity,
                                   [FromForm] string unit, [FromForm] string address, [FromForm] string details, [FromForm] string date)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            ViewBag.ProductNameOrg = productName;
            ViewBag.PriceOrg = price;
            ViewBag.QuantityOrg = quantity;
            ViewBag.UnitOrg = unit;
            ViewBag.AddressOrg = address;
            ViewBag.DetailsOrg = details;

            if (string.IsNullOrEmpty(productName?.Trim()) || string.IsNullOrEmpty(price?.Trim()) || string.IsNullOrEmpty(quantity?.Trim())
                || string.IsNullOrEmpty(unit?.Trim()) || string.IsNullOrEmpty(address?.Trim()) || string.IsNullOrEmpty(details?.Trim()))
            {
                if (productName == null)
                    ViewBag.ProductName = "يجب كتابة اسم المنتج";
                if (price == null)
                    ViewBag.Price = "يجب تحديد سعر المنتج";
                if (quantity == null)
                    ViewBag.Quantity = "يجب تحديد الكمية";
                if (unit == null)
                    ViewBag.Unit = "يجب كتابة الوحدة";
                if (address == null)
                    ViewBag.Address = "يجب اضافة العنوان";
                if (details == null)
                    ViewBag.Details = "يجب اضافة تفاصيل للمنتج";

                return View();
            }

            foreach (char item in price)
            {
                if (!char.IsDigit(item))
                {
                    ViewBag.Price = "يجب أن يحتوي السعر على ارقام فقط";

                    return View();
                }
            }
            foreach (char item in quantity)
            {
                if (!char.IsDigit(item))
                {
                    ViewBag.Quantity = "يجب أن تدخل الكمية بالارقام فقط";

                    return View();
                }
            }
            db.Products.Add(new Product()
            {
                NameAr = productName.Trim(),
                Price = Convert.ToInt32(price),
                Quantity = Convert.ToInt32(quantity),
                Unit = unit,
                DetailsAr = details.Trim(),
                Duration = 10,
                CreatedAt = DateTime.Now,
                Status = true,
                Views = 0,
                Discount = 0,
                Evaluation = 0
            }) ;

            db.SaveChanges();

            ViewBag.success = true;



            return View();
        }
        }
}
