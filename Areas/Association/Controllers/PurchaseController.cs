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
    public class PurchaseController : Controller
    {
        /*  private IRepository<Purchase> purchaseRepository;
          private IRepository<Product> productRepository;*/
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseController(/*IRepository<Purchase> purchaseRepository, IRepository<Product> productRepository,*/ IUnitOfWork unitOfWork)
        {
            /*this.purchaseRepository = purchaseRepository;
            this.productRepository = productRepository;*/
            _unitOfWork = unitOfWork;

        }

        public IActionResult index()
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
            List<Product> products = new List<Product>();
            var users = _unitOfWork.GetRepository<User>().Find(a => (a.UsersId == int.Parse(HttpContext.Session.GetString("_AssUserId"))) || (a.Id == int.Parse(HttpContext.Session.GetString("_AssUserId"))));
            foreach (var user in users)
            {
                products.AddRange(_unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).Where(i => i.UserId == user.Id));
            }
            if (id == 0)
            {
                var model = new PurchaseViewModel
                {
                    products = products.ToList(),
                    purchase = new Purchase
                    {
                        Id = id
                    },

                };
                return View(model);
            }
            else
            {
                var model = new PurchaseViewModel
                {
                    products = products.ToList(),
                    purchase = _unitOfWork.GetRepository<Purchase>().Include(p => p.Products, c => c.PaymentItems).SingleOrDefault(a => a.Id == id),
                };
                return View(model);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, PurchaseViewModel purchaseViewModel, string[] products, [FromForm] int UserId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (id == 0)
                    {
                        purchaseViewModel.purchase.Id = id;
                        purchaseViewModel.purchase.UserId = UserId;
                        purchaseViewModel.purchase.CreatedAt = DateTime.Now;
                        List<Product> list = new List<Product>();

                        foreach (var item in products)
                        {
                            int productId = int.Parse(item.Substring(0, item.LastIndexOf('-')));
                            var product = _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).FirstOrDefault(i => i.Id == productId);
                            list.Add(product);
                        }
                        purchaseViewModel.purchase.Products = list;
                        _unitOfWork.GetRepository<Purchase>().Add(purchaseViewModel.purchase);
                        _unitOfWork.GetRepository<Purchase>().SaveChanges();
                        return Json(new { status = "success", type = "purchase", html = Helper.RenderRazorViewToString(this, "PurchaseTable"), messgaeTitle = "إضافة طلب", messageBody = "تمت إضافة الطلب بنجاح" });
                    }
                    else
                    {
                        purchaseViewModel.purchase.Id = id;
                        purchaseViewModel.purchase.UserId = UserId;
                        purchaseViewModel.purchase.CreatedAt = DateTime.Now;
                        List<Product> list = new List<Product>();
                        foreach (var item in products)
                        {
                            int productId = int.Parse(item.Substring(0, item.LastIndexOf('-')));
                            var product = _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).FirstOrDefault(i => i.Id == productId);
                            list.Add(product);
                        }
                        purchaseViewModel.purchase.Products = list;
                        _unitOfWork.GetRepository<Purchase>().Update(purchaseViewModel.purchase);
                        _unitOfWork.GetRepository<Purchase>().SaveChanges();
                        return Json(new { status = "success", type = "purchase", html = Helper.RenderRazorViewToString(this, "PurchaseTable"), messgaeTitle = "تعديل طلب", messageBody = "تمت تعديل الطلب بنجاح" });

                    }
                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", type = "purchase", html = Helper.RenderRazorViewToString(this, "PurchaseTable"), messgaeTitle = "إضافة طلب", messageBody = "حدث خطأ أثناء إضافة/تعديل الطلب" });
                    //return Json(new { status = "error", html = Helper.RenderRazorViewToString(this, "ProductsTable") });

                }


            }
            else
            {
                List<Product> productslist = new List<Product>();
                var users = _unitOfWork.GetRepository<User>().Find(a => (a.UsersId == int.Parse(HttpContext.Session.GetString("_AssUserId"))) || (a.Id == int.Parse(HttpContext.Session.GetString("_AssUserId"))));
                foreach (var user in users)
                {
                    productslist.AddRange(_unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).Where(i => i.UserId == user.Id));
                }
                purchaseViewModel.purchase.Id = id;
                var model = new PurchaseViewModel
                {
                    products = productslist.ToList(),
                    purchase = purchaseViewModel.purchase ?? new Purchase
                    {
                    },

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }

            //return Json(new { status = "success", html = Helper.RenderRazorViewToString(this, "ProductsTable") });
        }


        public IActionResult ShowProducts(int? id)
        {
            var paymentItems = _unitOfWork.GetRepository<PaymentItem>().Include(p => p.Product, Pu => Pu.Purchase).Where(a => a.PurchaseId == id);
            return View(paymentItems);
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
                _unitOfWork.GetRepository<Purchase>().Remove(_unitOfWork.GetRepository<Purchase>().GetById(id));
                _unitOfWork.GetRepository<Purchase>().SaveChanges();
                return Json(new { status = "success", type = "purchase", html = Helper.RenderRazorViewToString(this, "PurchaseTable", null), messgaeTitle = "حذف طلب", messageBody = "تم حذف الطلب بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable", null), messgaeTitle = "حذف طلب", messageBody = "حدث خطأ أثناء حذف الطلب" });
            }
        }
        public IActionResult GetProducts(string q)
        {
            /*  List<Product> products = new List<Product>();
              var users = _unitOfWork.GetRepository<User>().Find(a => (a.UsersId == int.Parse(HttpContext.Session.GetString("_AssUserId"))) || (a.Id == int.Parse(HttpContext.Session.GetString("_AssUserId"))));
              foreach (var user in users)
              {
                  products.AddRange();
              }*/

            IEnumerable<Models.SelectListItem> productsList = Enumerable.Empty<Models.SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                productsList = _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).Where(p => p.NameAr.Contains(q)&&p.UserId== int.Parse(HttpContext.Session.GetString("_AssUserId"))).Select(
                   u => new Models.SelectListItem
                   {
                       Text = u.NameAr,
                       Id = u.Id,
                       Amount = u.Price
                   }
                   );
            return Json(new { items = productsList });
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
                IQueryable<Purchase> purchaseData = _unitOfWork.GetRepository<Purchase>().Include(p => p.Products, u => u.User).Where(a=>a.User.Id== int.Parse(HttpContext.Session.GetString("_AssUserId"))||a.User.UsersId== int.Parse(HttpContext.Session.GetString("_AssUserId")));
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