using E_commerce.Infersructure;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;

namespace e_commerce.Areas.Association.Controllers
{
    [Area("Association")]
    public class UsersController : Controller
    {
        //private IRepository<User> usersRepository;
        // private IRepository<Place> places;
        //private IRepository<Phone> phone;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(/*IRepository<User> usersRepository, IRepository<Place> placesRepository, IRepository<Phone> phoneRepository,*/IUnitOfWork unitOfWork)
        {
            //this.usersRepository = usersRepository;
            // this.places = placesRepository;
            // this.phone = phoneRepository;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("_AssUserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Notificatins = _unitOfWork.GetRepository<Notification>().Include(u => u.user).Where(a => a.IsRead == false && a.user.UsersId == int.Parse(userId)).ToList();
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_AssUserId");
            return RedirectToAction("Index", "Login");

        }

        public IActionResult UserProfile(int id = 0)
        {
            var user = HttpContext.Session.GetString("_AssUserId");
            var userId = int.Parse(user);
            ViewBag.Notificatins = _unitOfWork.GetRepository<Notification>().Include(u => u.user).Where(a => a.IsRead == false && a.user.UsersId == userId).ToList();




            var model = new UserViewModel
            {
                places = _unitOfWork.GetRepository<Place>().GetAll().ToList(),
                user = _unitOfWork.GetRepository<User>().Include(p => p.Place, ph => ph.Phone, u => u.Users).SingleOrDefault(i => i.Id == userId),

            };
            model.user.Password = "";
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
                    userViewModel.user.Password = Hashpassword.Hashedpassword(userViewModel.user.Password);
                    _unitOfWork.GetRepository<User>().Update(userViewModel.user);
                    _unitOfWork.GetRepository<User>().SaveChanges();
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
                    places = _unitOfWork.GetRepository<Place>().GetAll().ToList(),
                    user = new User
                    {
                        Id = 0
                    },

                };
            }
            return RedirectToAction("UserProfile");

        }
        public IActionResult GetUser(string q)
        {
            IEnumerable<SelectListItem> usersList = Enumerable.Empty<SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                usersList = _unitOfWork.GetRepository<User>().Include(a => a.Place, p => p.Phone).Where(i => i.Id == int.Parse(HttpContext.Session.GetString("_AssUserId") ?? "1") || i.UsersId == int.Parse(HttpContext.Session.GetString("_AssUserId") ?? "1") && i.Name.Contains(q)).Select(
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
                _unitOfWork.GetRepository<User>().Remove(_unitOfWork.GetRepository<User>().GetById(id));
                _unitOfWork.GetRepository<User>().SaveChanges();
                return Json(new { status = "success", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable", null), messgaeTitle = "حذف المستخدم", messageBody = "تم حذف المستخدم بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable", null), messgaeTitle = "حذف المستخدم", messageBody = "حدث خطأ أثناء حذف المستخدم" });
            }
        }
        private IEnumerable<User> getAllUsers(int page = 1, string name = "")
        {
            int UserID = int.Parse(HttpContext.Session.GetString("_AssUserId") ?? "1");
            const int pageSize = 10;
            if (page < 1)
                page = 1;
            int Count = _unitOfWork.GetRepository<User>().Include(p => p.Place, Ph => Ph.Phone).Where(i => i.Id == UserID || i.UsersId == UserID && i.Name.Contains(name)).Count();
            var pagingInfo = new PagingInfo(Count, page, pageSize);
            pagingInfo.PageName = "Users";
            int recSkip = (page - 1) * pageSize;
            var data = _unitOfWork.GetRepository<User>().Include(p => p.Place, Ph => Ph.Phone).Where(i => i.Id == UserID || i.UsersId == UserID && i.Name.Contains(name)).Skip(recSkip).Take(pagingInfo.ItemsPerPage).ToList();
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
                IQueryable<User> usersData = _unitOfWork.GetRepository<User>().Include(p => p.Place, Ph => Ph.Phone).Where(i => (i.Id == int.Parse(HttpContext.Session.GetString("_AssUserId")) || (i.UsersId == int.Parse(HttpContext.Session.GetString("_AssUserId")))));
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
