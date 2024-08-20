using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    public class ChangePhoneController : Controller
    {
        //WebContext db;
        private readonly IUnitOfWork _unitOfWork;
        public ChangePhoneController(/*WebContext db*/IUnitOfWork unitOfWork)
        {
            // this.db = db;
            _unitOfWork = unitOfWork;
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
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string newPhone)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;

            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            if (string.IsNullOrEmpty(newPhone?.Trim()))
            {
                if (newPhone == null)
                    ViewBag.Errorphone = "من فضلك قم بإدخال رقم الهاتف";

                return View();
            }

            /* التحقق من أن رقم الهاتف ارقام فقط */
            foreach (char item in newPhone)
            {
                if (!char.IsDigit(item))
                {
                    ViewBag.Errorphone = "رقم الهاتف غير صحيح، يجب ان يكون ارقام فقط";

                    return View();
                }
            }

            if (newPhone.Length != 9)
            {
                ViewBag.Errorphone = "رقم الهاتف غير صحيح، يرجى التأكد من رقم الهاتف";

                return View();
            }

            if (!newPhone.StartsWith("77") && !newPhone.StartsWith("73") && !newPhone.StartsWith("71") && !newPhone.StartsWith("70"))
            {
                ViewBag.Errorphone = "رقم الهاتف غير صحيح، يجب أن يكون رقم الهاتف يمني";

                return View();
            }

            Phone phoneRow = _unitOfWork.GetRepository<Phone>().FirstOrDefault(ph => ph.Number == newPhone);

            if (phoneRow != null)
            {
                ViewBag.Errorphone = "الرقم موجود مسبقاً";

                return View();
            }

            int? phoneId = HttpContext.Session.GetInt32("phoneId");

            Phone phoneO = _unitOfWork.GetRepository<Phone>().FirstOrDefault(p => p.Id == phoneId);

            if (phoneO == null)
            {
                ViewBag.Error = "من فضلك قم بالتواصل بالدعم الفني";
                return View();
            }

            phoneO.Number = newPhone;
            bool result = _unitOfWork.GetRepository<Phone>().SaveChanges();
            if (result)
            {
                ViewBag.Error = "عذراً لم يتم تغيير رقم الهاتف يرجى التواصل مع الدعم الفني";
                return View();
            }

            //ViewBag.Success = "تم ارسال طلب التحقق";
            ViewBag.Success = "تم تغيير رقم الهاتف";
            ViewBag.phone = newPhone;

            return View();
        }
    }
}
