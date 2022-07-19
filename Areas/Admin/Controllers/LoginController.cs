using E_commerce.Models;
using E_commerce.Models.Repositories;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private IRepository<Phone> phone;

        public LoginController(IRepository<Phone> phoneRepository)
        {
            this.phone = phoneRepository;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel loginViewModel)
        {
           
            Phone phoneUser = phone.Find(loginViewModel.Number);
            if (phoneUser.User.Password == loginViewModel.Password)
            {
                HttpContext.Session.SetString("_UserId", phoneUser.User.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
