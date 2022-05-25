using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        IUsersRepository<Product> products;

        public ProductsController(IUsersRepository<Product> repository)
        {
            products = repository;
        }
        public IActionResult Index()
        {
            IList<Product> product = products.List();
            return View(product);
        }
        //GET Create
        public IActionResult Create()
        {

            return View();
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
            var pro = products.Find(id);

            return View(pro);
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
