using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    public class DetailsAutionController : Controller
    {
        WebContext db;
        public DetailsAutionController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string id)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;

            if (id == null)
            {
                return Redirect("/home");
            }
            getAutionData(id);
            return View();
        }

        [HttpPost]
        public IActionResult Index(string id, [FromForm] string comment)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");

            if (id == null)
            {
                return Redirect("/home");
            }

            int userId = HttpContext.Session.GetInt32("idS") ?? 0;
            if (userId == 0)
            {
                return Redirect("/login");
            }

            if (!string.IsNullOrEmpty(comment))
            {
                int auctionId = Convert.ToInt32(id);
                int productId = db.Auctions.FirstOrDefault(a => a.Id == auctionId).ProductId;
                db.Comments.Add(new Comment()
                {
                    Text = comment,
                    ProductId = productId,
                    UserId = userId,
                });
                db.SaveChanges();
            }

            getAutionData(id);
            return View();
        }

        public IActionResult Bidding(string id, [FromForm] int? bidding)
        {
            if (id == null)
                return Redirect("/home");

            if (bidding == null || bidding < 1)
            {
                return Redirect("/DetailsAution?id=" + id);
            }
            try
            {
                int userId = HttpContext.Session.GetInt32("idS") ?? 0;
                int autionId = Convert.ToInt32(id);
                int ammount = Convert.ToInt32(bidding);

                AuctionsUser auctions = db.AuctionsUsers.FirstOrDefault(a => a.AuctionId == autionId);
                if(auctions == null)
                {
                    db.AuctionsUsers.Add(new AuctionsUser()
                    {
                        AuctionId = autionId,
                        UserId = userId,
                        Amount = ammount
                    });
                }
                else
                {
                    if(ammount > auctions.Amount)
                    {
                        auctions.UserId = userId;
                        auctions.Amount = ammount;
                    }
                }
                
                db.SaveChanges();
            }
            catch (Exception)
            { }

            return Redirect("/DetailsAution?id=" + id);
        }
        public void getAutionData(string id)
        {
            try
            {
                ViewBag.autionId = id;

                int auctionId = Convert.ToInt32(id);

                var data = db.Auctions.Join( // first table 
              db.Products, //second table
              a => a.ProductId, // first table key
              p => p.Id, // second table key
              (a, p) => new { product = p, aution = a } // new data
              )
              .FirstOrDefault(ss => ss.aution.Id == auctionId); // where condition

                ViewBag.images = db.ImagesProducts.Where(ip => ip.ProductId == data.product.Id).ToList();
                if (data != null)
                {
                    ViewBag.Auction = new AutionsProduct() { Auctions = data.aution, Products = data.product };
                    ViewBag.maxAmount = db.AuctionsUsers.FirstOrDefault(au => au.AuctionId == auctionId)?.Amount;
                    List<Comment> comments = db.Comments.Where(cmd => cmd.ProductId == data.product.Id).Take(10).OrderByDescending(cmd => cmd.Id).ToList();
                    List<UsersWithComments> commentsList = new List<UsersWithComments>();
                    foreach (var comment in comments)
                    {
                        commentsList.Add(new UsersWithComments()
                        {
                            user = db.Users.FirstOrDefault(us => us.Id == comment.UserId),
                            comment = comment,
                        });

                    }
                    ViewBag.comments = commentsList;
                }
            }
            catch (Exception)
            {
            }


        }
    }
}
