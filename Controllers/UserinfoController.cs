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

            getTableData();

            return View();
        }


        [HttpPost]
        public IActionResult index([FromForm] IFormFile image, [FromForm] string newName, [FromForm] string newAddress)
        {
            initLayout();
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            int userId = HttpContext.Session.GetInt32("idS") ?? 0;

            // Update image
            if (image != null)
            {
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
            getTableData();

            return View();
        }

        private void getTableData()
        {
            int userId = HttpContext.Session.GetInt32("idS") ?? 0;
            ViewBag.prodActive = db.Products.Where(p => p.Status == "فعال" && p.UserId == userId).ToList();
            ViewBag.prodStop = db.Products.Where(p => p.Status == "متوقف" && p.UserId == userId).ToList();
            ViewBag.prodOrder = db.Products.Where(p => p.PurchaseId != null && p.UserId == userId).ToList();
            ViewBag.prodPurchased = db.Products.Join( // first table 
                db.Payments, //second table
                a => a.PurchaseId, // first table key
                p => p.PurchaseId, // second table key
                (a, p) => a // new data
                )
                .Where(p => p.PurchaseId != null && p.UserId == userId) // where condition
                .ToList();// converto to list, so we can use it in for

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

        
        public IActionResult Delete(int? id)
        {
            initLayout();
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

<<<<<<< Updated upstream
        public JsonResult GetProductPurchase()
        {
            var id = HttpContext.Session.GetString("_UserId");
            var p = db.Products.Include(p => p.Category).Where(p => p.Status == "فعال" && p.PurchaseId >0 && p.UserId == int.Parse(id));
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ProductTable", p) });
=======
            if(id != null && id > 0)
            {
                Product product = db.Products.FirstOrDefault(p => p.Id == id);
                if(product != null)
                {
                    List<ImagesProduct> images = db.ImagesProducts.Where(p => p.ProductId == id).ToList();
                    foreach (var img in images)
                    {
                        db.ImagesProducts.Remove(img);
                    }
                    db.SaveChanges();

                    List<Comment> comments = db.Comments.Where(p => p.ProductId == id).ToList();
                    foreach (var cmt in comments)
                    {
                        db.Comments.Remove(cmt);
                    }
                    db.SaveChanges();

                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
            getTableData();

            return Redirect("/UserInfo");
>>>>>>> Stashed changes
        }

    }
}
