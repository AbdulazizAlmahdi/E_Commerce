//using E_commerce.Migrations;
using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace E_commerce.Controllers
{
    public class AutionsController : Controller
    {
        //WebContext db;
        private readonly IUnitOfWork _unitOfWork;
        public AutionsController(/*WebContext db,*/IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;

            // var auctio = db.Auctions.ToList();


            //  berfor unitofwork
            /*         var autionList = db.Auctions.Join( // first table 
                         db.Products, //second table
                         a => a.ProductId, // first table key
                         p => p.Id, // second table key
                         (a, p) => new { product = p, aution = a } // new data
                         )
                         .Where(ss => ss.aution.EndDate >= DateTime.Now) // where condition
                         .ToList();// converto to list, so we can use it in for*/

            var autionList = await _unitOfWork.GetRepository<Auction>().Include(a => a.Product.ImagesProducts, d => d.Product.Directorate.Governorate).Where(a => a.EndDate >= DateTime.Now).ToListAsync();

            /*  List <AutionsProduct> autonProducts = new List<AutionsProduct>();

              foreach (var item in autionList)
              {
                  autonProducts.Add(new AutionsProduct() { Auctions = item.aution, Products = item.product });
              }
  */
            return View(autionList);
        }


    }
}
