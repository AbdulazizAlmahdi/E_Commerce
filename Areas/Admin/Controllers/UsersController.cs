using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models.ViewModels;
using E_commerce.ViewModel;
using E_commerce;
using static e_commerce.Helper;

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IRepository<User> users;
        private IRepository<Place> places;
        private IRepository<Phone> phone;

        public UsersController(IRepository<User> usersRepository, IRepository<Place> placesRepository, IRepository<Phone> phoneRepository)
        {
            this.users = usersRepository;
            this.places = placesRepository;
            this.phone = phoneRepository;
        }
        public IActionResult Index(int page = 1, String name = "")
        {
            return View(getAllUsers(page, name));
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
                        Id = 0
                    },

                };
                return View(model);
            }
            else
            {
                var model = new UserViewModel
                {
                    places = places.show(null).ToList(),
                    user = users.Find(id),

                };
                return View(model);
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        users.Add(userViewModel.user);

                    }
                    else
                    {
                        userViewModel.user.UsersId = Convert.ToInt32(UsersId);
                        userViewModel.user.UpdatedAt = DateTime.Now;
                        users.Update(userViewModel.user);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", getAllUsers()) });

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
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }
        public IActionResult GetUser(string q)
        {
            IEnumerable<SelectListItem> usersList = Enumerable.Empty<SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                usersList = users.show(int.Parse(HttpContext.Session.GetString("_UserId") ?? "1")).Where(u => u.Name.Contains(q)).Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Id = u.Id
                    }
                    );
            return Json(new { items = usersList });
        }

        public ActionResult Delete(int id)
        {
            users.Delete(id);
            return RedirectToAction("Index");
        }
        private IEnumerable<User> getAllUsers(int page = 1, string name = "")
        {
            int UserID = int.Parse(HttpContext.Session.GetString("_UserId") ?? "1");
            const int pageSize = 10;
            if (page < 1)
                page = 1;
            int Count = users.show(UserID, name).Count();
            var pagingInfo = new PagingInfo(Count, page, pageSize);
            pagingInfo.PageName = "Users";
            int recSkip = (page - 1) * pageSize;
            var data = users.show(UserID, name).Skip(recSkip).Take(pagingInfo.ItemsPerPage).ToList();
            this.ViewBag.PagingInfo = pagingInfo;
            return data;
        }
    }


}
