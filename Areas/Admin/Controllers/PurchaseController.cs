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
using static e_commerce.Helper;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PurchaseController : Controller
    {
        private IRepository<Purchase> purchaseRepository;
        private IRepository<Product> productRepository;

        public PurchaseController(IRepository<Purchase> purchaseRepository, IRepository<Product> productRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.productRepository = productRepository;
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

        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {

            if (id == 0)
            {
                var model = new PurchaseViewModel
                {
                    products = productRepository.show(null).ToList(),
                    purchase = new Purchase
                    {
                        Id=id
                    },

                };
                return View(model);
            }
            else
            {
                var model = new PurchaseViewModel
                {
                    products = productRepository.show(null).ToList(),
                    purchase = purchaseRepository.Find(id),
                };
                return View(model);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, PurchaseViewModel purchaseViewModel, string[] products)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (id == 0)
                    {
                        purchaseViewModel.purchase.Id = id;
                        purchaseViewModel.purchase.UserId = 1;
                        List<Product> list = new List<Product>();

                        foreach (var item in products)
                        {
                            int productId = int.Parse(item.Substring(0, item.LastIndexOf('-')));
                            var product = productRepository.Find(productId);
                            list.Add(product);
                        }
                        purchaseViewModel.purchase.Products = list;
                        purchaseRepository.Add(purchaseViewModel.purchase);
                        return Json(new { status = "success", type = "purchase", html = Helper.RenderRazorViewToString(this, "PurchaseTable"), messgaeTitle = "إضافة فاتورة", messageBody = "تمت إضافة الفاتورة بنجاح" });
                    }
                    else
                    {
                        return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "PurchaseTable"), messgaeTitle = "تعديل فاتورة", messageBody = "تمت تعديل الفاتورة بنجاح" });

                    }
                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable"), messgaeTitle = "إضافة مستخدم", messageBody = "حدث خطأ أثناء إضافة/تعديل مستخدم" });
                    //return Json(new { status = "error", html = Helper.RenderRazorViewToString(this, "ProductsTable") });

                }


            }
            else
            {
                var model = new PurchaseViewModel
                {

                    products = productRepository.show(null).ToList(),
                    purchase = new Purchase
                    {
                    },

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }

            //return Json(new { status = "success", html = Helper.RenderRazorViewToString(this, "ProductsTable") });
        }

        public IActionResult ShowProducts(int? id)
        {
            var products = purchaseRepository.Find(id ?? 0).Products;
            return View(products);
        }
        public IActionResult GetProducts(string q)
        {
            IEnumerable<Models.SelectListItem> productsList = Enumerable.Empty<Models.SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                productsList = productRepository.show(null).Where(p => p.NameAr.Contains(q)).Select(
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