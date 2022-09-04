//using E_commerce.Migrations;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
   
    public class HomeController : Controller
    {
        WebContext db;
        public HomeController(WebContext db)
        {
            this.db = db;
        }

        public IActionResult Index([FromQuery] string search, [FromQuery] string catId)
        {
            List<Product> products = new List<Product>();

            if (search != null)
                products = db.Products.Where(prod =>
                prod.PurchaseId == null &&
                prod.Status == "فعال"
                && (prod.NameAr.Trim().Contains(search.Trim())
                || prod.DetailsAr.Trim().Contains(search.Trim()))
                ).OrderByDescending(p => p.CreatedAt).ToList();
            else if (!string.IsNullOrEmpty(catId))
            {
                try
                {
                    int catIdInt = Convert.ToInt32(catId);
                    products = db.Products.Where(prod =>
                    prod.PurchaseId == null && 
                    prod.Status == "فعال" && 
                    prod.CategoryId == catIdInt)
                        .OrderByDescending(p => p.CreatedAt)
                        .ToList();
                }
                catch (Exception) { }
            }
            else
                products = db.Products.Where(prod => prod.PurchaseId == null && prod.Status == "فعال")
                    .OrderByDescending(p => p.CreatedAt)
                    .ToList();

            List<ProductsWithImages> newProducts = new List<ProductsWithImages>();
            foreach (var product in products)
            {
                if (product.UserId != null)
                {
                    product.UserId = db.Users.FirstOrDefault(u => u.Id == product.UserId)?.UsersId;
                }
                List<ImagesProduct> image = db.ImagesProducts.Where(img => img.ProductId == product.Id).ToList();
                newProducts.Add(new ProductsWithImages()
                {
                    product = product,
                    image = image,
                });
            }

            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;


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

            return View(newProducts);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userNameS");

            return Redirect("/home");
        }

        public IActionResult Privacy()
        {
            List<Help> help = db.Helps.ToList();
            List<Phone> users = db.Phones.ToList();

            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
