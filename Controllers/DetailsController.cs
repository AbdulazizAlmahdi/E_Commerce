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
    public class DetailsController : Controller
    {
        WebContext db;
        public DetailsController(WebContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string id)
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;

            int productId = Convert.ToInt32(id);

            //product details
            Product product = db.Products.FirstOrDefault(p => p.Id == productId);

            if(product == null)
            {
                return Redirect("/home");
            }

            //view
            product.Views++;
            db.SaveChanges();

            ProductsWithImages productDetials = new ProductsWithImages();

            if (product.UserId != null)
            {
                User user = db.Users.FirstOrDefault(u => u.Id == product.UserId);
                if (user != null)
                {
                    product.UserId = user.JobName == "عميل" ? 1 : 0;
                }
            }
            else
            {
                product.UserId = 0;
            }

            List<ImagesProduct> image = db.ImagesProducts.Where(img => img.ProductId == product.Id).ToList();
            productDetials.product = product;
            productDetials.image = image;

            ViewBag.Product = productDetials;


            //adds products the same
            List<Product> ProductList = db.Products.Where(pr => pr.NameAr.Trim().Contains(product.NameAr.Trim()) && pr.Id != product.Id).Take(10).ToList();

            List<ProductsWithImages> newProductsList = new List<ProductsWithImages>();
            foreach (var prod in ProductList)
            {
                
                image = db.ImagesProducts.Where(img => img.ProductId == product.Id).ToList();
                newProductsList.Add(new ProductsWithImages()
                {
                    product = product,
                    image = image,
                });
            }
            ViewBag.ProductList = newProductsList;

            //start comments
            List<Comment> comments = db.Comments.Where(cmd => cmd.ProductId == productId).Take(10).OrderByDescending(cmd => cmd.Id).ToList();
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


            
            return View();
        }

        [HttpPost]
        public IActionResult AddComments(string id, [FromForm] string comment)
        {
            try
            {
                int productId = Convert.ToInt32(id);

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
                    db.Comments.Add(new Comment()
                    {
                        Text = comment,
                        ProductId = productId,
                        UserId = userId,
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
            }

            return Redirect("/Details?id=" + id);
        }


        [HttpPost]
        public IActionResult AddToCart([FromForm] int? id, [FromForm] string name, [FromForm] double? price, [FromForm] string url) 
        {
            try
            {
                int productId = Convert.ToInt32(id);

                if (id != null && name != null && price != null /*&& url != null*/)
                {
                    Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
                    if(cart == null)
                    {
                        Cart.getInstance().Add(new Cart()
                        {
                            Id = (int)id,
                            Name = name,
                            Price = (int)price,
                            ImageURL = url,
                            Quantity = 1

                        });
                    } 
                    //else
                    //{
                    //    cart.Quantity++;
                    //}
                    
                }

                
            }
            catch (Exception)
            {
            }

            return Redirect("/Details?id=" + id);
        }


    }
}
