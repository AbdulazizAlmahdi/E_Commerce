using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_commerce.ViewModel;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;
using e_commerce;
using SelectListItem = E_commerce.Models.SelectListItem;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        IRepository<Category> categoryRepository;
        IRepository<User> userRepository;
        public CategoryController(IRepository<Category> categoryRepository,IRepository<User> userRepository)
        {
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
        }
        public ActionResult Index()

        {
            var userId = HttpContext.Session.GetString("_UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        [NoDirectAccess]
        public ActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new CategoryViewModel
                {
                    category = new Category
                    {                       
                    }

                };
                return View(model);
            }
            else
            {
                var model = new CategoryViewModel
                {
                    category = categoryRepository.Find(id),
                };
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(int id,CategoryViewModel categoryViewModel,string categoryId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        categoryViewModel.category.CreatedAt = DateTime.Now;
                        categoryViewModel.category.UserId = int.Parse(HttpContext.Session.GetString("_UserId"));
                        if(categoryId!=null){
                        categoryViewModel.category.CategoryId = Convert.ToInt32(categoryId);
                        }
                        categoryRepository.Add(categoryViewModel.category);
                        return Json(new { status = "success", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "إضافة صنف", messageBody = "تمت إضافة الصنف بنجاح" });
                    }
                    else
                    {
                        if (checkCategory(categoryViewModel.category.UserId ?? 0)) { 
                        categoryViewModel.category.UpdatedAt = DateTime.Now;
                        categoryViewModel.category.CategoryId = Convert.ToInt32(categoryId)==0?null: Convert.ToInt32(categoryId);
                        categoryViewModel.category.Id = id;
                        categoryRepository.Update(categoryViewModel.category);
                        return Json(new { status = "success", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "تعديل صنف", messageBody = "تمت تعديل الصنف بنجاح" });
                    }
                        else
                        {
                            return Json(new { status = "error", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "تعديل صنف", messageBody = "ليس لديك صلاحية" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    var x=ex.InnerException.Message;
                    return Json(new { status = "error", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "إضافة تصنيف", messageBody = "حدث خطأ أثناء إضافة/تعديل تصنيف" });
                }
            }
            else
            {
                var model = new CategoryViewModel
                {
                    category = new Category
                    {
                        Id=0,
                    }

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }
        bool checkCategory(int userId)
        {
            User user = GetChildUser().FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        List<User> GetChildUser()
        {
            var users = userRepository.show(int.Parse(HttpContext.Session.GetString("_UserId"))).ToList();

            return users;

        }

        [NoDirectAccess]
        public ActionResult Delete(int? id){
                  return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Delete(int id){
            try
            {
                Category category = categoryRepository.Find(id);
                if (checkCategory(category.UserId ?? 0))
                {
                    categoryRepository.Delete(id);
                return Json(new { status = "success", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "حذف صنف", messageBody = "تم حذف الصنف بنجاح" });
            }
                        else
            {
                return Json(new { status = "error", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "تعديل صنف", messageBody = "ليس لديك صلاحية" });
            }
        }
            catch (Exception e)
            {
                var x=e.InnerException.Message;
                return Json(new { status = "error", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "حذف صنف", messageBody = "حدث خطأ أثناء حذف الصنف" });
            }
         }
        public IActionResult GetCategory(string q)
        {
            IEnumerable<SelectListItem> categoriesList = Enumerable.Empty<SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                categoriesList = categoryRepository.show(0).Where(c => c.Name.Contains(q)).Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Id = u.Id
                    }
                    );
            return Json(new { items = categoriesList });
        }
        public IActionResult GetCategoryData()
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
                IQueryable<Category> categoriesData = categoryRepository.show(null);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    categoriesData = categoriesData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    categoriesData = categoriesData.Where(m => m.Name.Contains(searchValue));
                }
                recordsTotal = categoriesData.Count();
                var data = categoriesData.Skip(skip).Take(pageSize).ToList();
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
