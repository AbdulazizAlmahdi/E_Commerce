
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
        IRepository<Auction> auction;
        IRepository<Product> products;

        public AuctionsController(IRepository<Auction> auction, IRepository<Product> products)
        {
            this.products = products;
            this.auction = auction;
        }
        public IActionResult Index()
        {
            return View();
        }
        [NoDirectAccess]        
    
       public IActionResult GetProducts(string q)
        {
           IEnumerable<Models.SelectListItem> productsList = Enumerable.Empty<Models.SelectListItem>();
           if (!(string.IsNullOrEmpty(q) || string.IsNullOrWhiteSpace(q)))
                productsList = products.show(0).Where(p => p.NameAr.Contains(q)).Select(
                   p => new Models.SelectListItem
                   {
                       Text = p.NameAr,
                       Id = p.Id
                   }
                   );
           return Json(new { items = productsList});
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
                IQueryable<Auction> auctionsData = auction.show(1);
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


    }
}
