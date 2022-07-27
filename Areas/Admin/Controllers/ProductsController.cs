
using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;
using static e_commerce.Helper;
using e_commerce;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        IRepository<Product> products;
        IRepository<Category> categories;


        public ProductsController(IRepository<Product> products, IRepository<Category> categories)
        {
            this.categories = categories;
            this.products = products;
        }
        public IActionResult Index()
        {

            return View();
        }
        [NoDirectAccess]
        public IActionResult CreateOrEdit(int id = 0)
        {

            if (id == 0)
            {
                var model = new ProductViewModel
                {
                    categories = categories.show(null).ToList(),
                    Product = new Product
                    {
                    },

                };
                return View(model);
            }
            else
            {
                var model = new ProductViewModel
                {
                    categories = categories.show(null).ToList(),
                    Product = products.Find(id),

                };
                return View(model);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, ProductViewModel productViewModel, string ProductsId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0)
                    {
                        productViewModel.Product.Id = Convert.ToInt32(ProductsId);
                        productViewModel.Product.CreatedAt = DateTime.Now;
                        products.Add(productViewModel.Product);
                    }
                    else
                    {
                        productViewModel.Product.Id = Convert.ToInt32(ProductsId);
                        productViewModel.Product.Id = Convert.ToInt32(id);
                        productViewModel.Product.UpdatedAt = DateTime.Now;
                        products.Update(productViewModel.Product);

                    }
                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", html = Helper.RenderRazorViewToString(this, "ProductsTable") });
                }
                return Json(new { status = "success", html = Helper.RenderRazorViewToString(this, "ProductsTable") });

            }
            else
            {
                var model = new ProductViewModel
                {
                    categories = categories.show(null).ToList(),
                    Product = new Product
                    {
                        Id = 0
                    },

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }
        }

        //public IActionResult Delete(int id)
        //{
        //    ProductViewModel productViewModel = new ProductViewModel
        //    {
        //        GetCategory = products.GetCategories().Select(c => new Models.SelectListItem
        //        {

        //        }),

        //        Product = products.Find(id)
        //    };

        //    return View(productViewModel);


        //}
        ////GET Create
        //public IActionResult Create()
        //{
        //    ProductViewModel productViewModel = new ProductViewModel
        //    {
        //        GetCategory = products.GetCategories().Select(c => new Models.SelectListItem
        //        {

        //        }),
        //    };

        //    return View(productViewModel);
        //}
        ////post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            try
            {
                products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }


        }
        //public IActionResult GetProduct(string q)

        //{
        //    IEnumerable<Models.SelectListItem> poroductList = Enumerable.Empty<Models.SelectListItem>();
        //    if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
        //        poroductList = products.show(int.Parse(HttpContext.Session.GetString("_ProductId") ?? "1")).Where(u => u.NameAr.Contains(q)).Select(
        //            u => new Models.SelectListItem
        //            {
        //                Text = u.NameAr,
        //                Id = u.Id
        //            }
        //            );
        //    return Json(new { items = poroductList });
        //}
        public IActionResult GetProductData()
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
                IQueryable<Product> productsData = products.show(1);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    productsData = productsData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    productsData = productsData.Where(m => m.NameAr.Contains(searchValue)
                                                || m.NameEn.Contains(searchValue)
                                                );
                }
                recordsTotal = productsData.Count();
                var data = productsData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Product product)
        {

            // products.Delete(ID);
            return RedirectToAction(nameof(Index));



        }


    }
}
