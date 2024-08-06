using E_commerce.Infersructure;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_commerce.Controllers
{
    public class LoginController : Controller
    {
        // WebContext db;
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(/*WebContext db*/IUnitOfWork unitOfWork)
        {
            // this.db = db;
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string phone, [FromForm] string password)
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            ViewBag.PhoneOrg = phone;

            if (string.IsNullOrEmpty(phone?.Trim()))
            {
                ViewBag.Error = "يجب تعبئه حقل رقم الهاتف";

                return View();
            }
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

            var phoneRow = _unitOfWork.GetRepository<Phone>().FirstOrDefault(ph => ph.Number == phone);

            if (phoneRow == null)
            {
                ViewBag.Error = "رقم الهاتف غير مسجل بعد";

                return View();
            }
            var user = _unitOfWork.GetRepository<User>().Include(d => d.Directorate.Governorate).FirstOrDefault(i => i.PhoneId == phoneRow.Id);
            if (user == null || !(Hashpassword.Verify(password, user.Password)))
            {
                ViewBag.Error = "رقم الهاتف أو كلمة المرور غير صحيح";

                return View();
            }
            if (user.Status == "متوقف")
            {
                ViewBag.Error = "يرجى تفعيل الحساب من قبل الادمن";

                return View();
            }


            HttpContext.Session.SetString("phoneS", phone);
            HttpContext.Session.SetInt32("phoneId", phoneRow.Id);
            HttpContext.Session.SetString("userNameS", user.Name ?? "مستخدم");
            HttpContext.Session.SetInt32("idS", user.Id);
            HttpContext.Session.SetString("userAddress", user.Address ?? "لايوجد عنوان");
            HttpContext.Session.SetString("userAddressD", user.Directorate.Name ?? "لايوجد عنوان");
            HttpContext.Session.SetString("userAddressG", user.Directorate.Governorate.Name ?? "لايوجد عنوان");

            var imageRow = _unitOfWork.GetRepository<ImagesUser>().Find(img => img.UserId == user.Id).ToList().LastOrDefault();
            if (imageRow != null)
                HttpContext.Session.SetString("userImage", imageRow.ImageUrl);
            else
                HttpContext.Session.SetString("userImage", "users/default.png");

            return Redirect("/home");
        }
    }
}