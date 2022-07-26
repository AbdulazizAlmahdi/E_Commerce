using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models.ViewModels;
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
        public CategoryController(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public ActionResult Index()

        {
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
                        categoryViewModel.category.CategoryId = Convert.ToInt32(categoryId);
                        categoryRepository.Add(categoryViewModel.category);
                                        return Json(new { status = "success", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "إضافة تصنيف", messageBody = "تمت إضافة التصنيف بنجاح" });
                    }
                    else
                    {
                        categoryViewModel.category.UpdatedAt = DateTime.Now;
                        categoryViewModel.category.CategoryId = Convert.ToInt32(categoryId)==0?null: Convert.ToInt32(categoryId);
                        categoryViewModel.category.Id = id;
                        categoryRepository.Update(categoryViewModel.category);
                        return Json(new { status = "success", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "تعديل تصنيف", messageBody = "تمت تعديل التصنيف بنجاح" });
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
        [NoDirectAccess]
        public ActionResult Delete(int? id){
                  return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Delete(int id){
            try
            {
                categoryRepository.Delete(id);
                return Json(new { status = "success", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "حذف تصنيف", messageBody = "تم حذف التصنيف بنجاح" });

            }
            catch (Exception e)
            {
                var x=e.InnerException.Message;
                return Json(new { status = "error", type = "category", html = Helper.RenderRazorViewToString(this, "CategoryTable"), messgaeTitle = "حذف تصنيف", messageBody = "حدث خطأ أثناء حذف التصنيف" });
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
                IQueryable<Category> usersData = categoryRepository.show(1);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    usersData = usersData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    usersData = usersData.Where(m => m.Name.Contains(searchValue));
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
