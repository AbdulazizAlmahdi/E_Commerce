using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace E_commerce.Controllers
{
    public class UserinfoController : Controller
    {
        [Obsolete]
        private readonly IHostingEnvironment hosting;
        private readonly IUnitOfWork _unitOfWork;


        [Obsolete]
        public UserinfoController(IHostingEnvironment hosting, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            this.hosting = hosting;
        }

        private void initLayout()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.phones = HttpContext.Session.GetString("phoneS");
            ViewBag.userAddress = HttpContext.Session.GetString("userAddress");
            ViewBag.userAddressD = HttpContext.Session.GetString("userAddressD");
            ViewBag.userAddressG = HttpContext.Session.GetString("userAddressG");
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

                    _unitOfWork.GetRepository<ImagesUser>().Add(new ImagesUser()
                    {
                        ImageUrl = imageName,
                        UserId = userId
                    });

                    _unitOfWork.GetRepository<ImagesUser>().SaveChanges();
                }

                HttpContext.Session.SetString("userImage", imageName);
                ViewBag.userImage = imageName;
            }
            //Update user info
            else if (newName != null && newAddress != null)
            {
                User user = _unitOfWork.GetRepository<User>().Include(d => d.Directorate.Governorate).FirstOrDefault(u => u.Id == userId);
                if (user != null)
                {
                    user.Name = newName;
                    user.Address = newAddress;

                    if (_unitOfWork.GetRepository<User>().SaveChanges())
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
            ViewBag.prodActive = _unitOfWork.GetRepository<Purchase>().Find(p => p.Status == false && p.UserId == userId).ToList();
            ViewBag.prodStop = _unitOfWork.GetRepository<Purchase>().Find(p => p.Status == true && p.UserId == userId).ToList();
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
        public IActionResult Details(int id)
        {
            initLayout();
            var model = _unitOfWork.GetRepository<PaymentItem>().Include(p => p.Purchase, d => d.Product).Where(p => p.PurchaseId == id).ToList();
            return View(model);

        }

        public IActionResult Delete(int? id)
        {
            initLayout();


            if (id != null && id > 0)
            {
                Purchase purchase = _unitOfWork.GetRepository<Purchase>().Include(p => p.PaymentItems).FirstOrDefault(p => p.Id == id);
                if (purchase != null)
                {


                    _unitOfWork.GetRepository<PaymentItem>().RemoveRange(purchase.PaymentItems);

                    _unitOfWork.GetRepository<PaymentItem>().SaveChanges();


                    _unitOfWork.GetRepository<Purchase>().Remove(purchase);
                    _unitOfWork.GetRepository<Product>().SaveChanges();
                }
            }
            getTableData();

            return Redirect("/UserInfo");

        }

    }
}
