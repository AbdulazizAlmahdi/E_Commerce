﻿using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class ChangePassController : Controller
    {
        WebContext db;
        public ChangePassController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            return View();

        }

        [HttpPost]
        public IActionResult Index([FromForm] string oldPass, [FromForm] string newPass, [FromForm] string newPass2)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");

            if (ViewBag.userS == null)
            {
                return Redirect("/home");
            }

            if (oldPass == null || newPass == null || newPass2 == null)
            {
                if (oldPass == null)
                    ViewBag.ErrorOldPass = "من فضلك قم بكتابة كلمة المرور القديمة";
                if (newPass == null)
                    ViewBag.ErrorNewPass = "من فضلك قم بكتابة كلمة المرور الجديدة";
                if (newPass2 == null)
                    ViewBag.ErrorNewPass2 = "من فضلك قم بتكرار كتابة كلمة المرور";

                return View();
            }

            if (newPass.Length <6)
            {
                ViewBag.ErrorNewPass = "عزيزي المستخدم يجب أن تكون كلمة المرور أكثر من خمسة أرقام";
                return View();
            }

            if (newPass != newPass2)
            {
                ViewBag.ErrorNewPass2 = "كلمة المرور ليست متساوية";
                return View();
            }

            if (oldPass == newPass)
            {
                ViewBag.ErrorNewPass = "كلمة المرور الجديدة مشابهة لكلمة المرور الحالية";
                ViewBag.ErrorOldPass = "كلمة المرور الحالية مشابهة لكلمة المرور الجديدة";
                return View();
            }

            int? userId = HttpContext.Session.GetInt32("idS");

            User user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                ViewBag.Error = "من فضلك قم بالتواصل بالدعم الفني";
                return View();
            }

            if (user.Password != oldPass)
            {
                ViewBag.ErrorOldPass = "كلمة المرور الحالية غير صحيحة";
                return View();
            }
           

            user.Password = newPass;
            int result = db.SaveChanges();
            if (result == 0)
            {
                ViewBag.Error = "عذراً لم يتم تغيير كلمة المرور يرجى التواصل مع الدعم الفني";
                return View();
            }
            ViewBag.Success = "تم تغيير كلمة المرور بنجاح";

            return View();

        }
    }
}
