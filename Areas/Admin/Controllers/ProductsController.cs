
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
        IProductRepository<Product> products;
        public int PageSize = 6;

        public ProductsController(IProductRepository<Product> repository) => this.products = repository;
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 10;
            if (page < 1)
                page = 1;
            int Count = products.Products.Count();
            var pagingInfo = new PagingInfo(Count, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            var data = products.Products.Skip(recSkip).Take(pagingInfo.ItemsPerPage).ToList();
            this.ViewBag.PagingInfo = pagingInfo;
            return View(data);
        }

    }
}
