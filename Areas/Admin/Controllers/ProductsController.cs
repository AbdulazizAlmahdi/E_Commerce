﻿using e_commerce;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static e_commerce.Helper;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        /*private IRepository<Product> products;
        private IRepository<ImagesProduct> imagesRepository;
        private IRepository<Category> categories;*/
        private readonly IWebHostEnvironment hosting;
        private readonly IUnitOfWork _unitOfWork;



        public ProductsController(/*IRepository<Product> products, IRepository<ImagesProduct> imagesRepository, IRepository<Category> categories,*/ IWebHostEnvironment hosting, IUnitOfWork unitOfWork)
        {
            /*this.imagesRepository = imagesRepository;
            this.categories = categories;
            this.products = products;*/
            this.hosting = hosting;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            ViewBag.Notificatins = _unitOfWork.GetRepository<Notification>().Include(u => u.user).Where(a => a.IsRead == false).ToList();
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
                    product = _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts, f => f.Farmer).FirstOrDefault(i => i.Id == id),
                };
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrEdit(int id, ProductsViewModel productViewModel, string CategoryId, [FromForm] string FarmerId)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (id == 0)
                    {
                        productViewModel.product.Id = id;
                        foreach (IFormFile item in productViewModel.Files ?? new List<IFormFile>())
                        {
                            productViewModel.product.ImagesProducts.Add(new ImagesProduct
                            {
                                ImageUrl = UploadFile(item)
                            });
                        }
                        productViewModel.product.CategoryId = Convert.ToInt32(CategoryId);
                        productViewModel.product.FarmerId = Convert.ToInt32(FarmerId);
                        productViewModel.product.Id = 0;
                        productViewModel.product.UserId = int.Parse(HttpContext.Session.GetString("_UserId"));
                        productViewModel.product.CreatedAt = DateTime.Now;
                        productViewModel.product.NameEn = productViewModel.product.NameAr;
                        productViewModel.product.DetailsEn = productViewModel.product.DetailsAr;
                        productViewModel.product.Views = 0;
                        productViewModel.product.Evaluation = 5;
                        _unitOfWork.GetRepository<Product>().Add(productViewModel.product);
                        _unitOfWork.GetRepository<Product>().SaveChanges();
                        return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable"), messgaeTitle = "إضافة المنتج", messageBody = "تمت إضافة المنتج بنجاح" });
                    }
                    else
                    {
                        var product = _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts,f=>f.Farmer).FirstOrDefault(i => i.Id == id);
                        if (productViewModel.Files != null && productViewModel.Files.Count > 0)
                        {
                            foreach (var item in product.ImagesProducts)
                            {
                                DeleteFile(item.ImageUrl);
                                _unitOfWork.GetRepository<ImagesProduct>().Remove(_unitOfWork.GetRepository<ImagesProduct>().GetById(item.Id));

                            }
                            _unitOfWork.GetRepository<ImagesProduct>().SaveChanges();
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
                        product.Status = productViewModel.product.Status;
                        product.CategoryId = Convert.ToInt32(CategoryId);
                        product.FarmerId = Convert.ToInt32(FarmerId);
                        product.UpdatedAt = DateTime.Now;
                        product.Address = productViewModel.product.Address;
                        product.DirectorateId = productViewModel.product.DirectorateId;
                        product.Discount = productViewModel.product.Discount;
                        product.Unit = productViewModel.product.Unit;
                        product.Price = productViewModel.product.Price;
                        product.Quantity = productViewModel.product.Quantity;
                        product.Duration = productViewModel.product.Duration;
                        _unitOfWork.GetRepository<Product>().Update(product);
                        _unitOfWork.GetRepository<Product>().SaveChanges();
                        return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable"), messgaeTitle = "تعديل منتج", messageBody = "تمت تعديل المنتج بنجاح" });

                    }
                }
                catch (Exception e)
                {
                    var exception = e.InnerException.Message;
                    return Json(new { status = "error", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable"), messgaeTitle = "إضافة منتج", messageBody = "حدث خطأ أثناء إضافة/تعديل المنتج" });
                    //return Json(new { status = "error", html = Helper.RenderRazorViewToString(this, "ProductsTable") });

                }


            }
            else
            {
                productViewModel.product.Id = id;
                var model = new ProductsViewModel
                {
                    product = productViewModel.product ?? new Product
                    {
                        Id = id
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
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
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
                _unitOfWork.GetRepository<Product>().Remove(_unitOfWork.GetRepository<Product>().GetById(id));
                _unitOfWork.GetRepository<Product>().SaveChanges();
                return Json(new { status = "success", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable", null), messgaeTitle = "حذف منتج", messageBody = "تم حذف المنتج بنجاح" });

            }
            catch (Exception e)
            {
                return Json(new { status = "error", type = "product", html = Helper.RenderRazorViewToString(this, "ProductsTable", null), messgaeTitle = "حذف منتج", messageBody = "حدث خطأ أثناء حذف المنتج" });
            }
        }
        public IActionResult GetCategory(string q)
        {
            IEnumerable<Models.SelectListItem> categorytList = Enumerable.Empty<Models.SelectListItem>();
            if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                categorytList = _unitOfWork.GetRepository<Category>().Include(c => c.categories).Where(p => p.Name.Contains(q)).Select(
                   u => new Models.SelectListItem
                   {
                       Text = u.Name,
                       Id = u.Id
                   }
                   );
            return Json(new { items = categorytList });
        }
        public async Task<IActionResult> GetProductData()
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
                //TODO
                // List<Product> products = new List<Product>();
                var products = await _unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts, d => d.Directorate.Governorate).ToListAsync();
                // var users = _unitOfWork.GetRepository<User>().GetAll();// (a =>( a.UsersId == int.Parse(HttpContext.Session.GetString("_UserId")))||(a.Id==int.Parse(HttpContext.Session.GetString("_UserId"))));
                /* foreach (var user in users)
                 {
                     products.AddRange(_unitOfWork.GetRepository<Product>().Include(c => c.Category, i => i.ImagesProducts).Where(i => i.UserId == user.Id));
                 }*/
                IQueryable<Product> productsData = products.AsQueryable();
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
