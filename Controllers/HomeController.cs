//using E_commerce.Migrations;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{

    public class HomeController : Controller
    {
        //WebContext db;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(/*WebContext db,*/IUnitOfWork unitOfWork)
        {
            //  this.db = db;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index([FromQuery] string search, [FromQuery] string catId)
        {
            List<Product> products = new List<Product>();

            if (search != null)
                products = _unitOfWork.GetRepository<Product>().Find(prod =>
               prod.Quantity != 0 &&
               prod.Status == "فعال"
               && (prod.NameAr.Trim().Contains(search.Trim())
               || prod.DetailsAr.Trim().Contains(search.Trim()))
                ).OrderByDescending(p => p.CreatedAt).ToList();
            else if (!string.IsNullOrEmpty(catId))
            {
                try
                {
                    int catIdInt = Convert.ToInt32(catId);
                    products = _unitOfWork.GetRepository<Product>().Find(prod =>
                    prod.PurchaseId == null &&
                    prod.Status == "فعال" &&
                    prod.Quantity!=0&&
                    prod.CategoryId == catIdInt)
                        .OrderByDescending(p => p.CreatedAt)
                        .ToList();
                }
                catch (Exception) { }
            }
            else
                products = _unitOfWork.GetRepository<Product>().Find(prod => prod.Quantity != 0 && prod.Status == "فعال")
                    .OrderByDescending(p => p.CreatedAt)
                    .ToList();

            List<ProductsWithImages> newProducts = new List<ProductsWithImages>();
            foreach (var product in products)
            {
                if (product.UserId != null)
                {
                    User user = _unitOfWork.GetRepository<User>().FirstOrDefault(u => u.Id == product.UserId);
                    if (user != null)
                    {
                        product.UserId = user.JobName == "عميل" ? 1 : 0;
                    }
                }
                else
                {
                    product.UserId = 0;
                }
                List<ImagesProduct> image = _unitOfWork.GetRepository<ImagesProduct>().Find(img => img.ProductId == product.Id).ToList();
                newProducts.Add(new ProductsWithImages()
                {
                    product = product,
                    image = image,
                });
            }

            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;
            var cate = _unitOfWork.GetRepository<Category>();

            List<Category> mainCat = cate.Find(cat => cat.CategoryId == null).ToList();
            List<MainCategory> categories = new List<MainCategory>();
            foreach (Category cat in mainCat)
            {
                List<Category> subCat = _unitOfWork.GetRepository<Category>().Find(sub => sub.CategoryId == cat.Id).ToList();

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
            Cart.getInstance().Clear();

            return Redirect("/home");
        }

        public async Task<IActionResult> Privacy()
        {
            var help = _unitOfWork.GetRepository<Help>().GetAll().ToList();
            var users = _unitOfWork.GetRepository<Phone>().GetAll().ToList();

            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
