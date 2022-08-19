
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
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private IRepository<Product> products;
        private IRepository<ImagesProduct> imagesRepository;
        private IRepository<Category> categories;
        private readonly IWebHostEnvironment hosting;


        public ProductsController(IRepository<Product> products, IRepository<ImagesProduct> imagesRepository, IRepository<Category> categories, IWebHostEnvironment hosting)
        {
            this.categories = categories;
            this.products = products;
            this.hosting = hosting;
            this.imagesRepository = imagesRepository;
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
                var model = new ProductsViewModel
                {
                    product = new Product
                    {

                    },

                };
                return View(model);
            }
            else
            {
                var model = new ProductsViewModel
                {
                    product = products.Find(id),
                };
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, ProductsViewModel productViewModel, string CategoryId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (id == 0)
                    {
                        productViewModel.product.Id = id;
                        foreach (IFormFile item in productViewModel.Files)
                        {
                            productViewModel.product.ImagesProducts.Add(new ImagesProduct
                            {
                                ImageUrl = UploadFile(item)
                            });
                        }
                        productViewModel.product.CategoryId = Convert.ToInt32(CategoryId);
                        productViewModel.product.Id = 0;
                        productViewModel.product.CreatedAt = DateTime.Now;
                        productViewModel.product.NameEn = productViewModel.product.NameAr;
                        productViewModel.product.DetailsEn = productViewModel.product.DetailsAr;
                        productViewModel.product.Views = 0;
                        productViewModel.product.Evaluation = 5;
                        products.Add(productViewModel.product);
                        return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable"), messgaeTitle = "إضافة مستخدم", messageBody = "تمت إضافة المستخدم بنجاح" });
                    }
                    else
                    {
                        var product = products.Find(id);
                        if (productViewModel.Files!=null&&productViewModel.Files.Count > 0)
                        {
                             foreach (var item in product.ImagesProducts)
                            {
                                DeleteFile(item.ImageUrl);
                               imagesRepository.Delete(item.Id);
                            }
                            product.ImagesProducts.Clear();
                            foreach (IFormFile item in productViewModel.Files)
                            {
                                product.ImagesProducts.Add(new ImagesProduct
                                {
                                    ImageUrl = UploadFile(item)
                                });
                            }
                        }
                        product.NameEn = productViewModel.product.NameEn;
                        product.NameAr = productViewModel.product.NameAr;
                        product.DetailsEn = productViewModel.product.DetailsEn;
                        product.DetailsAr = productViewModel.product.DetailsAr;
                        product.CategoryId = Convert.ToInt32(CategoryId);
                        product.UpdatedAt = DateTime.Now;
                        products.Update(product);
                        return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable"), messgaeTitle = "تعديل مستخدم", messageBody = "تمت تعديل المستخدم بنجاح" });

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
                var model = new ProductsViewModel
                {
                    product = new Product
                    {
                        Id = 0
                    },

                };
                return Json(new { status = "validation-error", html = Helper.RenderRazorViewToString(this, "CreateOrEdit", model) });
            }

            //return Json(new { status = "success", html = Helper.RenderRazorViewToString(this, "ProductsTable") });
        }
        public string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create)){
                    file.CopyTo(stream);
                    stream.Close();
                }
                return file.FileName;
            }

            return null;
        }   

        void DeleteFile(string fileName)
        {
                 string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, fileName);
            if ((System.IO.File.Exists(fullPath)))
            {
                    System.IO.File.Delete(fullPath);
            }
        }
        //GetFileFromUrl
        // public IFormFile GetFileFromUrl(String fileName)
        // {
        //    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
        //         string fullPath = Path.Combine(uploads, fileName);
        //    using (var stream = System.IO.File.OpenRead(fullPath))
        //    {
        //        return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
        //    }

        // }
        [NoDirectAccess]
        public ActionResult Delete(int? id)
        {
            return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string x)
        {
            try
            {
                products.Delete(id);
                return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable", null), messgaeTitle = "حذف المنتج", messageBody = "تم حذف المنتج بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable", null), messgaeTitle = "حذف المنتج", messageBody = "حدث خطأ أثناء حذف المنتج" });
            }
        }
        public IActionResult GetCategory(string q)
        {
            IEnumerable<Models.SelectListItem> categorytList = Enumerable.Empty<Models.SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                categorytList = categories.show(0).Where(p => p.Name.Contains(q)).Select(
                   u => new Models.SelectListItem
                   {
                       Text = u.Name,
                       Id = u.Id
                   }
                   );
            return Json(new { items = categorytList });
        }
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


    }
}
