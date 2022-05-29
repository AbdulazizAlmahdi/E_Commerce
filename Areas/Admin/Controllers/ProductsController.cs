
using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
<<<<<<< HEAD
        IProductRepository products;
        public int PageSize = 6;

        public ProductsController(IProductRepository repository) => this.products = repository;
       
        public IActionResult Index(int pageNumber = 1)
=======
        IProductRepository<Product> products;

        public ProductsController(IProductRepository<Product> repository)
>>>>>>> d91aedd9e2654f83d75f7f3ecd5e51d44d00eca5
        {
            var product= products.List().OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize);
            ProductViewModel productViewModel = new ProductViewModel
            {

                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = PageSize,
                    TotalItems = products.List().Count()
                },
                
                
               
            };

            return View(productViewModel);
        }
        public IActionResult Detailes(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                 GetCategory = products.GetCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
               Product=products.Find(id)
            };
            return View(productViewModel);
        }
        //GET Create
        public IActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                GetCategory = products.GetCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
            };

            return View(productViewModel);
        }
        //post create
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
        //GEt
        public IActionResult Delete(int id)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                GetCategory = products.GetCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),

                Product = products.Find(id)
            };

            return View(productViewModel);
          

        }
        
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(Product product)
        {
            
                products.Delete(product);   
                return RedirectToAction(nameof(Index));
            
           

        }
    }
}
