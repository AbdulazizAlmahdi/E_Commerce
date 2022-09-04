﻿using E_commerce.Models;
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
        private IRepository<Phone> phoneRepository;

        public LoginController(IRepository<Phone> phoneRepository)
        {
            this.phoneRepository = phoneRepository;

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
                Phone phoneUser = phoneRepository.Find(loginViewModel.Number);
                if (phoneUser == null) {
                    return View(new LoginViewModel { Status=false});
                }
               else if (phoneUser.User.Password == loginViewModel.Password)
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
