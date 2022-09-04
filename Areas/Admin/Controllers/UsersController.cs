using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.ViewModel;
using E_commerce;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IRepository<User> usersRepository;
        private IRepository<Place> places;
        private IRepository<Phone> phone;

        public UsersController(IRepository<User> usersRepository, IRepository<Place> placesRepository, IRepository<Phone> phoneRepository)
        {
            this.usersRepository = usersRepository;
            this.places = placesRepository;
            this.phone = phoneRepository;
        }
        public IActionResult Index()
        {
            //var userId = HttpContext.Session.GetString("_UserId");

            //if (userId == null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserId");
            return RedirectToAction("Index", "Login");

        }
        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {

            if (id == 0)
            {
                var model = new UserViewModel
                {
                    places = places.show(null).ToList(),
                    user = new User
                    {
                        Phone = new Phone(),
                    },

                };
                return View(model);
            }
            else
            {
                var model = new UserViewModel
                {
                    places = places.show(null).ToList(),
                    user = usersRepository.Find(id),

                };
                return View(model);
            }


        }


        public IActionResult UserProfile(int id = 0)
        {
            var user= HttpContext.Session.GetString("_UserId");
            var userId = int.Parse(user);
           

            
                var model = new UserViewModel
                {
                    places = places.show(null).ToList(),
                    user = usersRepository.Find(userId),

                };
                return View(model);
            
    

        }

        [HttpPost]

        public IActionResult EditProfile(int id, UserViewModel userViewModel, string UsersId)
        {
            if (ModelState.IsValid)
            {
               // try
                {
                         
                        //userViewModel.user.UsersId = Convert.ToInt32(UsersId);
                        userViewModel.user.Id = Convert.ToInt32(id);
                        userViewModel.user.UpdatedAt = DateTime.Now;
                        usersRepository.Update(userViewModel.user);
                    return RedirectToAction("UserProfile");                  
                }
               /* catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                }*/

            }
            else
            {
                var model = new UserViewModel
                {
                    places = places.show(null).ToList(),
                    user = new User
                    {
                        Id = 0
                    },

                };
            }
            return RedirectToAction("UserProfile");

        }

        public IActionResult CreateOrEdit(int id, UserViewModel userViewModel, string UsersId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {

                        userViewModel.user.UsersId = Convert.ToInt32(UsersId);
                        userViewModel.user.CreatedAt = DateTime.Now;
                        usersRepository.Add(userViewModel.user);
                        return Json(new { status = "success", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable"), messgaeTitle = "إضافة مستخدم", messageBody = "تمت إضافة المستخدم بنجاح" });
                    }
                    else
                    {
                        userViewModel.user.UsersId = Convert.ToInt32(UsersId);
                        userViewModel.user.Id = Convert.ToInt32(id);
                        userViewModel.user.UpdatedAt = DateTime.Now;
                        usersRepository.Update(userViewModel.user);
                        return Json(new { status = "success", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable"), messgaeTitle = "تعديل مستخدم", messageBody = "تمت تعديل المستخدم بنجاح" });
                    }
                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable"), messgaeTitle = "إضافة مستخدم", messageBody = "حدث خطأ أثناء إضافة/تعديل مستخدم" });
                }

            }
            else
            {
                var model = new UserViewModel
                {
                    places = places.show(null).ToList(),
                    user = new User
                    {
                        Id = 0
                    },

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }
        public IActionResult GetUser(string q)
        {
            IEnumerable<SelectListItem> usersList = Enumerable.Empty<SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                usersList = usersRepository.show(int.Parse(HttpContext.Session.GetString("_UserId") ?? "1")).Where(u => u.Name.Contains(q)).Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Id = u.Id
                    }
                    );
            return Json(new { items = usersList });
        }

        [NoDirectAccess]
        public ActionResult Delete(int? id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                usersRepository.Delete(id);
                return Json(new { status = "success", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable", null), messgaeTitle = "حذف المستخدم", messageBody = "تم حذف المستخدم بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable", null), messgaeTitle = "حذف المستخدم", messageBody = "حدث خطأ أثناء حذف المستخدم" });
            }
        }
        private IEnumerable<User> getAllUsers(int page = 1, string name = "")
        {
            int UserID = int.Parse(HttpContext.Session.GetString("_UserId") ?? "1");
            const int pageSize = 10;
            if (page < 1)
                page = 1;
            int Count = usersRepository.show(UserID, name).Count();
            var pagingInfo = new PagingInfo(Count, page, pageSize);
            pagingInfo.PageName = "Users";
            int recSkip = (page - 1) * pageSize;
            var data = usersRepository.show(UserID, name).Skip(recSkip).Take(pagingInfo.ItemsPerPage).ToList();
            this.ViewBag.PagingInfo = pagingInfo;
            return data;
        }

        public IActionResult GetUserData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                IQueryable<User> usersData = usersRepository.show(1);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    usersData = usersData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    usersData = usersData.Where(m => m.Name.Contains(searchValue)
                                                || m.Address.Contains(searchValue)
                                                || m.Phone.Number.Contains(searchValue));
                }
                recordsTotal = usersData.Count();
                var data = usersData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
