
using e_commerce;
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

namespace E_commerce.Areas.Association.Controllers
{
    [Area("Association")]
    public class AuctionsController : Controller
    {
        /* IRepository<Auction> auctionRepository;
         IRepository<Product> productsRepository;*/
        private readonly IUnitOfWork _unitOfWork;

        public AuctionsController(/*IRepository<Auction> auctionRepository, IRepository<Product> productsRepository, */IUnitOfWork unitOfWork)
        {
            /*this.productsRepository = productsRepository;
            this.auctionRepository = auctionRepository;*/
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
        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {

            if (id == 0)
            {
                var model = new AuctionsViewModel
                {
                    auction = new Auction
                    {
                        Id = 0,
                        Product = new Product()
                    },

                };
                return View(model);
            }
            else
            {
                var model = new AuctionsViewModel
                {
                    auction = _unitOfWork.GetRepository<Auction>().Include(p => p.Product).FirstOrDefault(i => i.Id == id),

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
                        _unitOfWork.GetRepository<Auction>().Add(auctionsViewModel.auction);
                        _unitOfWork.GetRepository<Auction>().SaveChanges();

                        if (auctionsViewModel.auction.ProductId != null)
                        {

                            Product product = _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).FirstOrDefault(i => i.Id == auctionsViewModel.auction.ProductId);
                            product.Status = "متوقف";
                            _unitOfWork.GetRepository<Product>().Update(product);
                            _unitOfWork.GetRepository<Product>().SaveChanges();
                        }
                        return Json(new { status = "success", type = "auctions", html = Helper.RenderRazorViewToString(this, "AuctionsTable"), messgaeTitle = "إضافة مزاد", messageBody = "تمت إضافة المزاد بنجاح" });
                    }
                    else
                    {
                        auctionsViewModel.auction.Id = id;
                        auctionsViewModel.auction.ProductId = Convert.ToInt32(ProductId);
                        _unitOfWork.GetRepository<Auction>().Update(auctionsViewModel.auction);
                        _unitOfWork.GetRepository<Auction>().SaveChanges();
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
                auctionsViewModel.auction.Id = id;
                var model = new AuctionsViewModel
                {
                    auction = auctionsViewModel.auction ?? new Auction
                    {
                        Id = 0,
                        Product = new Product()
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
                _unitOfWork.GetRepository<Auction>().Remove(_unitOfWork.GetRepository<Auction>().GetById(id));
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
                productsList = _unitOfWork.GetRepository<Product>().Find(u => u.UserId == int.Parse(HttpContext.Session.GetString("_AssUserId"))).Where(p => p.NameAr.Contains(q)).Select(
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
                IQueryable<Auction> auctionsData = _unitOfWork.GetRepository<Auction>().Include(a => a.AuctionsUsers, p => p.Product);
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
            //Auction auth = _unitOfWork.GetRepository<Auction>().Include(u => u.AuctionsUsers).FirstOrDefault(a => a.Id == id);
            var usersautions = _unitOfWork.GetRepository<AuctionsUser>().Include(u => u.User).Where(u => u.AuctionId == id).ToList();
            List<User> users = new List<User>();
            List<AuctionsViewModel> auctionsViewModel = new List<AuctionsViewModel>();
            foreach (var useraution in usersautions)
            {
                auctionsViewModel.Add(new AuctionsViewModel
                {
                    user = _unitOfWork.GetRepository<User>().Include(u => u.Phone).FirstOrDefault(u => u.Id == useraution.UserId),
                    auctionUser = useraution,
                    auction = null

                });

            }


            return View(auctionsViewModel);
        }


    }
}
