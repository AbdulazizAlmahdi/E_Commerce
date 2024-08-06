using E_commerce.Infersructure.Interface;
using E_commerce.Models;
using E_commerce.Models.Custome;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Controllers
{
    public class DetailsAutionController : Controller
    {
        // WebContext db;
        private readonly IUnitOfWork _unitOfWork;
        public DetailsAutionController(/*WebContext db,*/IUnitOfWork unitOfWork)
        {
            //this.db = db;
            _unitOfWork = unitOfWork;
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
                int productId = (int)_unitOfWork.GetRepository<Auction>().FirstOrDefault(a => a.Id == auctionId).ProductId;
                _unitOfWork.GetRepository<Comment>().Add(new Comment()
                {
                    Text = comment,
                    ProductId = productId,
                    UserId = userId,
                });
                _unitOfWork.GetRepository<Comment>().SaveChanges();
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

                AuctionsUser auctions = _unitOfWork.GetRepository<AuctionsUser>().FirstOrDefault(a => a.AuctionId == autionId && a.UserId == userId);
                if (auctions == null)
                {
                    _unitOfWork.GetRepository<AuctionsUser>().Add(new AuctionsUser()
                    {
                        AuctionId = autionId,
                        UserId = userId,
                        Amount = ammount
                    });
                    _unitOfWork.GetRepository<Notification>().Add(new Notification { Titel = "مشاركة جديدة في المزاد", Text = "هناك مشاركة جدبدة في المزاد رقم" + id, UserId = userId, Url = "Auctions/Index" });

                    _unitOfWork.GetRepository<AuctionsUser>().SaveChanges();
                }
                else
                {
                    if (ammount > auctions.Amount)
                    {
                        auctions.UserId = userId;
                        auctions.Amount = ammount;
                        _unitOfWork.GetRepository<AuctionsUser>().Update(auctions);
                        _unitOfWork.GetRepository<Notification>().Add(new Notification { Titel = "مشاركة جديدة في المزاد", Text = "هناك مشاركة جدبدة في المزاد رقم" + id, UserId = userId, Url = "Auctions/Index" });
                        _unitOfWork.GetRepository<AuctionsUser>().SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                e.GetBaseException();
            }

            return Redirect("/DetailsAution?id=" + id);
        }
        public void getAutionData(string id)
        {
            try
            {
                ViewBag.autionId = id;

                int auctionId = Convert.ToInt32(id);

                Auction auction = _unitOfWork.GetRepository<Auction>().Include(a => a.Product.ImagesProducts, d => d.Product.Directorate.Governorate).FirstOrDefault(ss => ss.Id == auctionId); // where condition

                //ViewBag.images = _unitOfWork.GetRepository<ImagesProduct>().Find(ip => ip.ProductId == auction.Product.Id).ToList();
                if (auction != null)
                {
                    ViewBag.Auction = auction;
                    try
                    {
                        ViewBag.maxAmount = _unitOfWork.GetRepository<AuctionsUser>().Find(au => au.AuctionId == auctionId).Max(au => au.Amount);
                    }
                    catch (Exception)
                    {
                        ViewBag.maxAmount = null;
                    }
                    List<Comment> comments = _unitOfWork.GetRepository<Comment>().Find(cmd => cmd.ProductId == auction.Product.Id).Take(10).OrderByDescending(cmd => cmd.Id).ToList();
                    List<UsersWithComments> commentsList = new();
                    foreach (var comment in comments)
                    {
                        commentsList.Add(new UsersWithComments()
                        {
                            user = _unitOfWork.GetRepository<User>().FirstOrDefault(us => us.Id == comment.UserId),
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
