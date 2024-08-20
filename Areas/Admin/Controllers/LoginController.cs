using E_commerce.Infersructure;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        // private IRepository<Phone> phoneRepository;
        private readonly IUnitOfWork _unitOfWork;


        public LoginController(/*IRepository<Phone> phoneRepository,*/IUnitOfWork unitOfWork)
        {
            // this.phoneRepository = phoneRepository;
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            return View(new LoginViewModel { Status = true });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
            try
            {
                Phone phoneUser = _unitOfWork.GetRepository<Phone>().Include(u => u.User).FirstOrDefault(n => n.Number == loginViewModel.Number);
                if (phoneUser == null)
                {
                    return View(new LoginViewModel { Status = false });
                }
                else if (Hashpassword.Verify(loginViewModel.Password, phoneUser.User.Password) && phoneUser.User.JobName == "مشرف عام" && phoneUser.User.Status != "متوقف")
                {
                    HttpContext.Session.SetString("_UserId", phoneUser.User.Id.ToString());
                    return RedirectToAction("Index", "Analytics");
                }
                else
                {
                    return View(new LoginViewModel { Status = false });
                }

            }
            catch (Exception e)
            {
                var x = e.InnerException;
            }
            return View();
        }
    }
}
