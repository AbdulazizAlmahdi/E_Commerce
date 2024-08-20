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
    public class BasketController : Controller
    {
        // WebContext db;
        private readonly IUnitOfWork _unitOfWork;
        public BasketController(/*WebContext db,*/IUnitOfWork unitOfWork)
        {
            // this.db = db;
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            ViewBag.userS = HttpContext.Session.GetString("userNameS");
            ViewBag.userImage = HttpContext.Session.GetString("userImage");
            ViewBag.cartCount = Cart.getInstance().Count;
            ViewBag.cart = Cart.getInstance();

            return View();
        }

        [HttpPost]
        public IActionResult Increment(int id)
        {
            Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
            if (cart != null)
                cart.Quantity++;
            return Redirect("/Basket");
        }

        [HttpPost]
        public IActionResult Decrement(int id)
        {
            Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
            if (cart != null)
                cart.Quantity--;
            return Redirect("/Basket");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            Cart cart = Cart.getInstance().FirstOrDefault(c => c.Id == id);
            if (cart != null)
                Cart.getInstance().Remove(cart);
            return Redirect("/Basket");
        }

        [HttpPost]
        public IActionResult Done([FromForm] String userAddress, [FromForm] String detials)
        {
            String phone = HttpContext.Session.GetString("phoneS");
            int userId = HttpContext.Session.GetInt32("idS") ?? 0;
            decimal total = 0;
            userAddress = userAddress ?? HttpContext.Session.GetString("userAddress");
            detials = detials ?? "";
            Purchase purchase = new Purchase()
            {
                Amount = 0,
                ExtraAmount = 0,
                Phone = phone,
                Address = userAddress,
                Detials = detials,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DeletedAt = DateTime.Now,
                UserId = userId,
            };
            List<PaymentItem> paymentItems = new List<PaymentItem>();
            foreach (var cart in Cart.getInstance())
            {

                paymentItems.Add(new PaymentItem()
                {
                    Amount = (decimal)cart.Price,
                    ProductId = cart.Id,
                    CreatedAt = DateTime.Now,
                    Quantity = cart.Quantity,
                    Purchase = purchase,
                });
                total += (cart.Quantity * ((decimal)cart.Price));
                //_unitOfWork.GetRepository<PaymentItem>().Add(paymentItem);
                Product product = _unitOfWork.GetRepository<Product>().FirstOrDefault(p => p.Id == cart.Id);
                if (product != null)
                {
                    // product.PurchaseId = purchase.Id;
                    product.Quantity = product.Quantity - cart.Quantity;
                    _unitOfWork.GetRepository<Product>().Update(product);
                }

                _unitOfWork.SaveChanges();
            }
            purchase.Amount = total;
            _unitOfWork.GetRepository<Purchase>().Add(purchase);
            _unitOfWork.SaveChanges();
            _unitOfWork.GetRepository<PaymentItem>().AddRange(paymentItems);
            _unitOfWork.GetRepository<Notification>().Add(new Notification {Titel="طلب شراء جديد", Text="هناك طلب شراء جدبد للمستخدم "+_unitOfWork.GetRepository<User>().GetById(userId).Name,UserId=userId,Url= "Purchase/Index"});
            _unitOfWork.SaveChanges();

            Cart.getInstance().Clear();
            return Redirect("/Basket");
        }
    }
}
