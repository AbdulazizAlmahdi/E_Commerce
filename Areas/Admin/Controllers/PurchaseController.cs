using E_commerce.Models;
using E_commerce.ViewModel;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using e_commerce;
using Microsoft.AspNetCore.Http;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseController : Controller
    {
        private IRepository<Purchase> purchaseRepository;

        public PurchaseController(IRepository<Purchase> purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        public IActionResult index()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            //TODO: change to ==
            if (userId != null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult GetPurchaseData()
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
                IQueryable<Purchase> purchaseData = purchaseRepository.show(1);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    purchaseData = purchaseData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {

                    purchaseData = purchaseData.Where(p => p.Detials.Contains(searchValue));
                }
                recordsTotal = purchaseData.Count();
                var data = purchaseData.Skip(skip).Take(pageSize).ToList();
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