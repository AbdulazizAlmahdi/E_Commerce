using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class AddProductController : Controller
    {
        WebContext db;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public AddProductController(WebContext db, IHostingEnvironment hosting)
        {
            this.db = db;
            this.hosting = hosting;
        }

        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;

            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            getCategories();

            return View();
        }


        [HttpPost]
        public IActionResult Index([FromForm] string productName, [FromForm] string price, [FromForm] string quantity,
                                   [FromForm] string unit, [FromForm] string address, [FromForm] string details, [FromForm] string date,
                                   [FromForm] IFormFile image1, [FromForm] IFormFile image2, [FromForm] IFormFile image3,
                                   string categoryId)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            int? userId = HttpContext.Session.GetInt32("idS");
            ViewBag.cartCount = Cart.getInstance().Count;

            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            getCategories();

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

            int categoryIdInt = Convert.ToInt32(categoryId);
            Product product = new Product()
            {
                NameAr = productName.Trim(),
                Price = Convert.ToInt32(price),
                Quantity = Convert.ToInt32(quantity),
                Address = address,
                Unit = unit,
                DetailsAr = details.Trim(),
                Duration = 10,
                CreatedAt = DateTime.Now,
                Status = "فعال",
                Views = 0,
                Discount = 0,
                Evaluation = 0,
                CategoryId = categoryIdInt,
                UserId = userId,
            };
            db.Products.Add(product);

            if (db.SaveChanges() > 0)
            {
                if (image1 != null)
                {
                    string fileNameImage1 = UpoadImages("f_", image1);
                    if (!string.IsNullOrEmpty(fileNameImage1))
                    {
                        db.ImagesProducts.Add(new ImagesProduct()
                        {
                            ImageUrl = fileNameImage1,
                            ProductId = product.Id,
                        });
                    }
                }

                if (image2 != null)
                {
                    string fileNameImage2 = UpoadImages("s_", image2);
                    if (!string.IsNullOrEmpty(fileNameImage2))
                    {
                        db.ImagesProducts.Add(new ImagesProduct()
                        {
                            ImageUrl = fileNameImage2,
                            ProductId = product.Id,
                        });
                    }
                }

                if (image3 != null)
                {
                    string fileNameImage3 = UpoadImages("th_", image3);
                    if (!string.IsNullOrEmpty(fileNameImage3))
                    {
                        db.ImagesProducts.Add(new ImagesProduct()
                        {
                            ImageUrl = fileNameImage3,
                            ProductId = product.Id,
                        });
                    }
                }

                db.SaveChanges();
            }


            ViewBag.success = true;

            return View();
        }
        private void getCategories()
        {
            List<Category> mainCat = db.Categories.Where(cat => cat.CategoryId == null).ToList();
            List<MainCategory> categories = new List<MainCategory>();
            foreach (Category cat in mainCat)
            {
                List<Category> subCat = db.Categories.Where(sub => sub.CategoryId == cat.Id).ToList();

                categories.Add(new MainCategory()
                {
                    Id = cat.Id,
                    Name = cat.Name,
                    subCategory = subCat
                });
            }
            ViewBag.categories = categories;
        }

        private string UpoadImages(string pref, IFormFile image)
        {
            string imageName = "";
            if (image != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads", "products");

                //Get extaintion by split by dot.
                string[] imagesParts = image.FileName.Split('.');
                string extantion = imagesParts[imagesParts.Length - 1];

                imageName = pref + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extantion;


                string fullPath = Path.Combine(uploads, imageName);
                image.CopyTo(new FileStream(fullPath, FileMode.Create));
            }

            return "products/" + imageName;
        }


    }


}
