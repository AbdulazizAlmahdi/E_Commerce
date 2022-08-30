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
    public class DetailsController : Controller
    {
        WebContext db;
        public DetailsController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string id)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");

            int productId = Convert.ToInt32(id);

            //product details
            Product product = db.Products.FirstOrDefault(p => p.Id == productId);

            ProductsWithImages productDetials = new ProductsWithImages();

            List<ImagesProduct> image = db.ImagesProducts.Where(img => img.ProductId == product.Id).ToList();
            productDetials.product = product;
            productDetials.image = image;

            ViewBag.Product = productDetials;


            //adds products the same
            List<Product> ProductList = db.Products.Where(pr => pr.NameAr.Trim().Contains(product.NameAr.Trim()) && pr.Id != product.Id).Take(10).ToList();

            List<ProductsWithImages> newProductsList = new List<ProductsWithImages>();
            foreach (var prod in ProductList)
            {
                image = db.ImagesProducts.Where(img => img.ProductId == product.Id).ToList();
                newProductsList.Add(new ProductsWithImages()
                {
                    product = product,
                    image = image,
                });
            }
            ViewBag.ProductList = newProductsList;

            return View();
        }

       
    }
}
