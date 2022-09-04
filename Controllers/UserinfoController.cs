using e_commerce;
using E_commerce.Models;
using E_commerce.Models.Custome;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class UserinfoController : Controller
    {
        WebContext db;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public UserinfoController(WebContext db, IHostingEnvironment hosting)
        {
            this.db = db;
            this.hosting = hosting;
        }

      private void initLayout()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.phones = HttpContext.Session.GetString("phoneS");
            ViewBag.userAddress = HttpContext.Session.GetString("userAddress");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;
        }    
        public IActionResult Index()
        {
            initLayout();
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            var vm = new UserInfoViewModel
            {
                GetProducts = db.Products.Include(p=>p.Category).Where(p => p.Status == "فعال")
            };

            return View(vm);
        }

                
        [HttpPost]
        public IActionResult index([FromForm] IFormFile image, [FromForm] string newName, [FromForm] string newAddress)
        {
            initLayout();
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            // Update image
            if (image != null)
            {
                int userId = Convert.ToInt32(HttpContext.Session.GetInt32("idS"));
                string imageName = UpoadImages(userId + "_", image);
                if (!string.IsNullOrEmpty(imageName))
                {

                    db.ImagesUsers.Add(new ImagesUser()
                    {
                        ImageUrl = imageName,
                        UserId = userId
                    });

                    db.SaveChanges();
                }

                HttpContext.Session.SetString("userImage", imageName);
                ViewBag.userImage = imageName;
            }
            //Update user info
            else if (newName != null && newAddress != null)
            {
                int userId = HttpContext.Session.GetInt32("idS") ?? 0;
                User user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.Name = newName;
                    user.Address = newAddress;

                    if (db.SaveChanges() > 0)
                    {
                        ViewBag.userS = user.Name;
                        ViewBag.userAddress = user.Address;

                        HttpContext.Session.SetString("userNameS", user.Name ?? "مستخدم");
                        HttpContext.Session.SetString("userAddress", user.Address ?? "لايوجد عنوان");
                    }
                }
            }

            initLayout();

            return View();
        }

        private string UpoadImages(string pref, IFormFile image)
        {
            string imageName = "default.png";
            if (image != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads", "users");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                //Get extaintion by split by dot.
                string[] imagesParts = image.FileName.Split('.');
                string extantion = imagesParts[imagesParts.Length - 1];

                imageName = pref + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + extantion;


                string fullPath = Path.Combine(uploads, imageName);
                image.CopyTo(new FileStream(fullPath, FileMode.Create));
            }

            return "users/" + imageName;
        }

        public JsonResult GetProduct(string state)
        {
            var id = HttpContext.Session.GetString("_UserId");
            var p = db.Products.Include(p => p.Category).Where(p => p.Status == state && p.PurchaseId==null && p.UserId == int.Parse(id));

            return Json(new {  html = Helper.RenderRazorViewToString(this, "_ProductTable",p) });

        }

        public JsonResult GetProductPurchase()
        {
            var p = db.Products.Include(p => p.Category).Where(p => p.Status == "فعال" && p.PurchaseId >0);

            return Json(new { html = Helper.RenderRazorViewToString(this, "_ProductTable", p) });

        }

    }
}
