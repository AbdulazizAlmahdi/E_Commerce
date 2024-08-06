using e_commerce;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpController : Controller
    {


        // private IRepository<Help> helpRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HelpController(/*IRepository<Help> helpRepository*/ IUnitOfWork unitOfWork)
        {
            //this.helpRepository = helpRepository;
            _unitOfWork = unitOfWork;
        }

        public IActionResult index()
        {
                        ViewBag.Notificatins= _unitOfWork.GetRepository<Notification>().Include(u=>u.user).Where(a => a.IsRead == false).ToList();

            var userId = HttpContext.Session.GetString("_UserId");

            if (userId == null)
            {
                return Redirect("Login/Index");
            }

            return View();
        }
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new HelpViewModel
                {
                    help = new Help(),
                };
                return View(model);
            }
            else
            {
                var model = new HelpViewModel
                {
                    help = _unitOfWork.GetRepository<Help>().GetById(id),
                };
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, HelpViewModel helpViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    helpViewModel.help.CreatedAt = DateTime.Now;
                    _unitOfWork.GetRepository<Help>().Add(helpViewModel.help);
                    _unitOfWork.GetRepository<Help>().SaveChanges();
                    return Json(new { status = "success", type = "help", html = RenderRazorViewToString(this, "HelpTable"), messgaeTitle = "إضافة مساعدة", messageBody = "تمت إضافة المساعدة بنجاح" });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", type = "help", html = Helper.RenderRazorViewToString(this, "HelpTable"), messgaeTitle = "إضافة مساعدة", messageBody = "حدث خطأ أثناء إضافة مساعدة" });

                }

            }
            else
            {
                helpViewModel.help.Id = id;
                var model = new HelpViewModel
                {
                    help = helpViewModel.help ?? new Help
                    {
                        Id = id
                    },

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }
        public IActionResult GetHelpData()
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
                IQueryable<Help> helpData = _unitOfWork.GetRepository<Help>().GetAll().AsQueryable();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    helpData = helpData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    switch (searchValue)
                    {
                        case "لم يتم حل المشكلة":
                            searchValue = "Pending";
                            break;
                        case "تم حل المشكلة":
                            searchValue = "Solved";
                            break;
                        case "تم تجاهل المشكلة":
                            searchValue = "Unsolved";
                            break;
                        default:
                            break;
                    }
                    helpData = helpData.Where(h => h.Subject.Contains(searchValue)
                                                || h.Details.Contains(searchValue)
                                                || h.Phone.Contains(searchValue)
                                                || h.Status.Contains(searchValue));
                }
                recordsTotal = helpData.Count();
                var data = helpData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [NoDirectAccess]
        public ActionResult IgnoreOrder(int? id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IgnoreOrder(int id)
        {
            try
            {
                Help help = _unitOfWork.GetRepository<Help>().GetById(id);
                help.Status = "Unsolved";
                _unitOfWork.GetRepository<Help>().Update(help);
                _unitOfWork.GetRepository<Help>().SaveChanges();
                return Json(new { status = "success", type = "help", html = Helper.RenderRazorViewToString(this, "HelpTable", null), messgaeTitle = "تجاهل طلب المساعدة", messageBody = "تم تجاهل طلب المساعدة" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", type = "help", html = Helper.RenderRazorViewToString(this, "HelpTable", null), messgaeTitle = "خطأ", messageBody = "حدث خطأ أثناء تجاهل طلب المساعدة" });
            }
        }
        public ActionResult SolveOrder(int? id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SolveOrder(int id)
        {
            try
            {
                Help help = _unitOfWork.GetRepository<Help>().GetById(id);
                help.Status = "Solved";
                _unitOfWork.GetRepository<Help>().Update(help);
                _unitOfWork.GetRepository<Help>().SaveChanges();
                return Json(new { status = "success", type = "help", html = Helper.RenderRazorViewToString(this, "HelpTable", null), messgaeTitle = "حل طلب المساعدة", messageBody = "تم حل طلب المساعدة" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", type = "help", html = Helper.RenderRazorViewToString(this, "HelpTable", null), messgaeTitle = "خطأ", messageBody = "حدث خطأ أثناء حل طلب المساعدة" });
            }
        }

    }
}