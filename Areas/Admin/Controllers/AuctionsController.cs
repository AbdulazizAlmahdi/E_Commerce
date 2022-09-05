
using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;
using e_commerce;
using E_commerce.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuctionsController : Controller
    {
        IRepository<Auction> auctionRepository;
        IRepository<Product> productsRepository;

        public AuctionsController(IRepository<Auction> auctionRepository, IRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
            this.auctionRepository = auctionRepository;
        }
        public IActionResult Index()
        {

            var userId = HttpContext.Session.GetString("_UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {

            if (id == 0)
            {
                var model = new AuctionsViewModel
                {
                    auction = new Auction
                    {
                        Product = new Product()
                    },

                };
                return View(model);
            }
            else
            {
                var model = new AuctionsViewModel
                {
                    auction = auctionRepository.Find(id),

                };
                return View(model);
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, AuctionsViewModel auctionsViewModel, string ProductId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        auctionsViewModel.auction.ProductId = Convert.ToInt32(ProductId);
                        auctionRepository.Add(auctionsViewModel.auction);
                        return Json(new { status = "success", type = "auctions", html = Helper.RenderRazorViewToString(this, "AuctionsTable"), messgaeTitle = "إضافة مزاد", messageBody = "تمت إضافة المزاد بنجاح" });
                    }
                    else
                    {
                        auctionsViewModel.auction.Id = id;
                        auctionsViewModel.auction.ProductId = Convert.ToInt32(ProductId);
                        auctionRepository.Update(auctionsViewModel.auction);
                        return Json(new { status = "success", type = "auctions", html = Helper.RenderRazorViewToString(this, "AuctionsTable"), messgaeTitle = "تعديل مستخدم", messageBody = "تمت تعديل المستخدم بنجاح" });
                    }

                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", type = "auctions", html = Helper.RenderRazorViewToString(this, "AuctionsTable"), messgaeTitle = "إضافة مزاد", messageBody = "حدث خطأ أثناء إضافة/تعديل مزاد" });

                }
            }
            else
            {
                var model = new AuctionsViewModel
                {
                    auction = new Auction
                    {
                        Id = 0
                    }
                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }
        [NoDirectAccess]
        public ActionResult Delete(int id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string x)
        {
            try
            {
                auctionRepository.Delete(id);
                return Json(new { status = "success", type = "auctions", html = Helper.RenderRazorViewToString(this, "AuctionsTable", null), messgaeTitle = "حذف المزاد", messageBody = "تم حذف المزاد بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "auctions", html = Helper.RenderRazorViewToString(this, "AuctionsTable", null), messgaeTitle = "حذف المزاد", messageBody = "حدث خطأ أثناء حذف المزاد" });
            }
        }

        public IActionResult GetProducts(string q)
        {
            IEnumerable<Models.SelectListItem> productsList = Enumerable.Empty<Models.SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                productsList = productsRepository.show(int.Parse(HttpContext.Session.GetString("_UserId"))).Where(p => p.NameAr.Contains(q)).Select(
                   p => new Models.SelectListItem
                   {
                       Text = p.NameAr,
                       Id = p.Id
                   }
                   );
            return Json(new { items = productsList });
        }

        public IActionResult GetAuctionsData()
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
                IQueryable<Auction> auctionsData = auctionRepository.show(int.Parse(HttpContext.Session.GetString("_UserId")));
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    auctionsData = auctionsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    auctionsData = auctionsData.Where(a => a.Product.NameAr.Contains(searchValue));
                }
                recordsTotal = auctionsData.Count();
                var data = auctionsData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [NoDirectAccess]
        public IActionResult ShowParticipants(int id)

        {
            Auction auth = auctionRepository.show(null).FirstOrDefault(a => a.Id == id);

            return View(auth);
        }


    }
}
