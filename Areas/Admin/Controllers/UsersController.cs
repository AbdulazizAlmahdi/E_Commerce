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

namespace e_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
       private IRepository<User> users;
       private IRepository<Place> places;
       private IRepository<Phone> phone;

        public UsersController(IRepository<User> usersRepository, IRepository<Place> placesRepository, IRepository<Phone> phoneRepository) {
           this.users = usersRepository; 
              this.places = placesRepository;
              this.phone = phoneRepository;
        } 
        public IActionResult Index(int page=1,String name= "")
        {
            int UserID = int.Parse(HttpContext.Session.GetString("_UserId")??"5");
            const int pageSize = 10;
            if (page < 1)
                page = 1;
            int Count = users.show(UserID, name).Count();
            var pagingInfo = new PagingInfo(Count, page, pageSize);
            pagingInfo.PageName = "Users";
          int  recSkip = (page - 1) * pageSize;
            var data = users.show(UserID, name).Skip(recSkip).Take(pagingInfo.ItemsPerPage).ToList();
            this.ViewBag.PagingInfo = pagingInfo;
            return View(data);
        }
        
        public IActionResult Create()
        {
            var model = new UserViewModel {
                places = places.show(null).ToList()
            };
            return View(model);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel userViewModel)
        {
            try
            {
                userViewModel.user.CreatedAt = DateTime.Now;
                    users.Add(userViewModel.user);
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }
        public IActionResult GetUser(string q)
        {
            IEnumerable<SelectListItem> usersList = Enumerable.Empty<SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                usersList = users.show(5).Where(u => u.Name.Contains(q)).Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id
                    }
                    );
            return Json(new { items = usersList });
        }
        public IActionResult Edit(int id)
        {
            
            var model = new UserViewModel
            {
                user = users.Find(id),
                places = places.show(null).ToList()
            };
            return View(model);
        }
    
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public IActionResult Search()
    {

        return Index(1,"");
    }
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public IActionResult Edit(UserViewModel userViewModel)
    {
        try
        {
            userViewModel.user.UpdatedAt = DateTime.Now;
            users.Update(userViewModel.user);
            return RedirectToAction("Index");
            }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel { RequestId = ex.Message });
        }
    }
        public ActionResult Delete(int id)
        {
            users.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
