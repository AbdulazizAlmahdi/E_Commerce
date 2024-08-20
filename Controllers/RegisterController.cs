using E_commerce.Infersructure;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Controllers
{
    public class RegisterController : Controller
    {
        // WebContext db;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterController(/*WebContext db*/IUnitOfWork unitOfWork)
        {
            // this.db = db;
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetGovernorate()
        {
            IEnumerable<Models.SelectListItem> GovernorateList = Enumerable.Empty<Models.SelectListItem>();

            GovernorateList = _unitOfWork.GetRepository<Governorate>().Include(c => c.Directorates).Select(
               u => new Models.SelectListItem
               {
                   Text = u.Name,
                   Id = u.Id
               }
               );
            return Json(new { items = GovernorateList });
        }

        public IActionResult GetDirectorate(int id)
        {
            IEnumerable<Models.SelectListItem> DirectorateList = Enumerable.Empty<Models.SelectListItem>();

            DirectorateList = _unitOfWork.GetRepository<Directorate>().Find(c => c.GovernorateId == id).Select(
               u => new Models.SelectListItem
               {
                   Text = u.Name,
                   Id = u.ID
               }
               );
            return Json(new { items = DirectorateList });
        }
        public IActionResult Index()
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            return View();
        }


        [HttpPost]
        public IActionResult Index([FromForm] string name, [FromForm] string address, [FromForm] string password, [FromForm] string password2, [FromForm] string phone, [FromForm] int DirectorateId)
        {
            ViewBag.cartCount = Cart.getInstance().Count;
            ViewBag.NameOrg = name;
            ViewBag.AddressOrg = address;
            ViewBag.PhoneOrg = phone;

            if (string.IsNullOrEmpty(phone?.Trim()))
            {
                ViewBag.Error = "يجب تعبئه حقل رقم الهاتف";

                return View();
            }

            /* التحقق من أن رقم الهاتف ارقام فقط */
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
            if (!phone.StartsWith("77") && !phone.StartsWith("73") && !phone.StartsWith("71") && !phone.StartsWith("70") && !phone.StartsWith("78"))
            {
                ViewBag.Error = "رقم الهاتف غير صحيح، يجب أن يكون رقم الهاتف يمني";

                return View();
            }

            /** ********************* **/
            Phone phoneRow = _unitOfWork.GetRepository<Phone>().FirstOrDefault(ph => ph.Number == phone);

            if (phoneRow != null)
            {
                ViewBag.Error = "الرقم موجود مسبقاً";

                return View();
            }

            /** ********************* **/
            _unitOfWork.GetRepository<Phone>().Add(new Phone()
            {
                Number = phone,
                CreatedAt = DateTime.Now,
            });


            /** ********************* **/


            if (password == null)
            {
                password = "123456";
                password2 = password;
            }
            else if (password != password2)
            {
                ViewBag.Error = "كلمةالمرور غير متساويه";
                return View();
            }
            name = name ?? ("User " + phone);
            address = address ?? "اليمن";
            _unitOfWork.GetRepository<Phone>().SaveChanges();
            phoneRow = _unitOfWork.GetRepository<Phone>().FirstOrDefault(ph => ph.Number == phone);
            if (phoneRow == null)
            {
                ViewBag.Error = "حدثت مشكلة يرجى التواصل مع الدعم الفني";
                return View();
            }
            _unitOfWork.GetRepository<User>().Add(new User()
            {
                Phone = phoneRow,
                Name = name,
                Address = address,
                Password = Hashpassword.Hashedpassword(password),
                CreatedAt = DateTime.Now,
                UsersId = null,
                JobName = StaticData.Roles[2].Name,
                Status = "متوقف",
                DirectorateId = DirectorateId
            });

            _unitOfWork.GetRepository<User>().SaveChanges();
            _unitOfWork.GetRepository<Notification>().Add(new Notification() {
                CreatedAt = DateTime.Now ,
                IsRead=false,
                Titel="تسجيل حساب جديد"
                ,
                Text="هناك تسجيل حساب جديد برقم هاتف"+phoneRow.Number+"للمستخدم "+name
            ,Url= "Users/Index"
            }
            );
            _unitOfWork.GetRepository<Notification>().SaveChanges();
            ViewBag.success = true;

            return View();
        }
    }
}
