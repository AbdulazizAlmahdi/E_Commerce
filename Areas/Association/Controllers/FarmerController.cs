using e_commerce;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;

namespace E_commerce.Areas.Association.Controllers
{
    [Area("Association")]
    public class FarmerController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public FarmerController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {        
            var userId =HttpContext.Session.GetString("_AssUserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Notificatins= _unitOfWork.GetRepository<Notification>().Include(u=>u.user).Where(a => a.IsRead == false&& a.user.UsersId== int.Parse(userId)).ToList();

            return View();
        }
        public IActionResult ShowProducts(int id)
        {
            var products = _unitOfWork.GetRepository<Farmer>().Include(u => u.Products, a => a.Directorate.Governorate).FirstOrDefault(a => a.Id == id);
            return View(products);
        }


        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {

            if (id == 0)
            {
                var farmer = new Farmer();

                return View(farmer);
            }
            else
            {
                var farmer = _unitOfWork.GetRepository<Farmer>().Include(a => a.Directorate.Governorate, b => b.User).FirstOrDefault(a => a.Id == id);

                return View(farmer);
            }


        }
        // POST: FarmersController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {

                        var userId = HttpContext.Session.GetString("_AssUserId");
                        farmer.UserId = Convert.ToInt32(userId);
                        _unitOfWork.GetRepository<Farmer>().Add(farmer);
                        _unitOfWork.SaveChanges();
                        return Json(new { status = "success", type = "farmer", html = Helper.RenderRazorViewToString(this, "FarmerTable"), messgaeTitle = "إضافة مزارع", messageBody = "تمت إضافة المزارع بنجاح" });



                        // return Json(new { status = "error", type = "user", html = Helper.RenderRazorViewToString(this, "UserTable"), messgaeTitle = "إضافة مزارع", messageBody = "رقم الهاتف موجود مسبقا " });


                    }
                    else
                    {
                        var userId = HttpContext.Session.GetString("_AssUserId");
                        farmer.UserId = Convert.ToInt32(userId);
                        farmer.UpdatedAt = DateTime.Now;
                        _unitOfWork.GetRepository<Farmer>().Update(farmer);
                        _unitOfWork.SaveChanges();
                        return Json(new { status = "success", type = "farmer", html = Helper.RenderRazorViewToString(this, "FarmerTable"), messgaeTitle = "تعديل مزارع", messageBody = "تمت تعديل المزارع بنجاح" });
                    }
                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", type = "farmer", html = Helper.RenderRazorViewToString(this, "FarmerTable"), messgaeTitle = "إضافة مزارع", messageBody = "حدث خطأ أثناء إضافة/تعديل مزارع" });
                }

            }
            else
            {
                farmer.Id = id;
                var model = farmer;
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }

        public IActionResult GetFarmer(string q)
        {
            var userId = HttpContext.Session.GetString("_AssUserId");

            IEnumerable<SelectListItem> farmersList = Enumerable.Empty<SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                farmersList = _unitOfWork.GetRepository<Farmer>().GetAll().Where(i => i.Name.Contains(q)&&i.UserId==Convert.ToInt32(userId)).Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Id = u.Id
                    }
                    );
            return Json(new { items = farmersList });
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

                _unitOfWork.GetRepository<Farmer>().Remove(_unitOfWork.GetRepository<Farmer>().GetById(id));
                _unitOfWork.SaveChanges();
                return Json(new { status = "success", type = "farmer", html = Helper.RenderRazorViewToString(this, "FarmerTable", null), messgaeTitle = "حذف المزارع", messageBody = "تم حذف المزارع بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "farmer", html = Helper.RenderRazorViewToString(this, "FarmerTable", null), messgaeTitle = "حذف المزارع", messageBody = "حدث خطأ أثناء حذف المزارع" });
            }
        }

        public IActionResult GetFarmerData()
        {
            var userId =Convert.ToInt32(HttpContext.Session.GetString("_AssUserId"));

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
                IQueryable<Farmer> farmersData = _unitOfWork.GetRepository<Farmer>().Include(d => d.Directorate.Governorate).Where(a=>a.UserId==userId).AsQueryable();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    farmersData = farmersData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    farmersData = farmersData.Where(m => m.Name.Contains(searchValue)
                                                || m.Address.Contains(searchValue)
                                                || m.PhoneNumber.Contains(searchValue)
                                                );
                }
                recordsTotal = farmersData.Count();
                var data = farmersData.Skip(skip).Take(pageSize).ToList();
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
