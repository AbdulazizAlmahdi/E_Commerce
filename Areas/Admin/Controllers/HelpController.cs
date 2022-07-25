using E_commerce.Models;
using E_commerce.ViewModel;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;
using e_commerce;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HelpController : Controller
    {


        private IRepository<Help> helpRepository;

        public HelpController(IRepository<Help> helpRepository)
        {
            this.helpRepository = helpRepository;
        }

        public IActionResult index()
        {
            return View();
        }
        public IActionResult CreateAndEdit(int id = 0)
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
                    help = helpRepository.Find(id),
                };
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAndEdit(HelpViewModel model)
        {
            if (ModelState.IsValid)
            {
                try{
                        model.help.CreatedAt = DateTime.Now;
                        model.help.status = false;
                        helpRepository.Add(model.help);
                        return Json(new { status="success",type="help",html=RenderRazorViewToString(this,"HelpTable"), messgaeTitle="إضافة مساعدة", messageBody="تمت إضافة المساعدة بنجاح" });
                }catch(Exception ex){
                 return Json(new { status = "error", type = "help", html = Helper.RenderRazorViewToString(this, "HelpTable"), messgaeTitle ="إضافة مساعدة" , messageBody = "حدث خطأ أثناء إضافة مساعدة" });

                }
                   
            }
            return View(model);
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
                IQueryable<Help> helpData = helpRepository.show(1);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    helpData = helpData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    helpData = helpData.Where(h => h.Subject.Contains(searchValue)
                                                || h.Details.Contains(searchValue)
                                                || h.Phone.Number.Contains(searchValue));
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
        public ActionResult IgnoreOrder(int id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IgnoreOrder(int id,string x)
        {
            try{                
            Help help = helpRepository.Find(id);
            help.status = true;
            helpRepository.Update(help);
            return Json(new { status = "success",type="help", html = Helper.RenderRazorViewToString(this, "HelpTable", null), messgaeTitle = "تجاهل طلب المساعدة", messageBody = "تم تجاهل طلب المساعدة" });
        }catch(Exception ex){
            return Json(new { status = "error",type="help", html = Helper.RenderRazorViewToString(this, "HelpTable", null), messgaeTitle = "خطأ", messageBody = "حدث خطأ أثناء تجاهل طلب المساعدة" });
        }
        }
    }
}