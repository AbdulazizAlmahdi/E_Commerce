using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        public readonly WebContext context;



        public AnalyticsRepository(WebContext context)
        {
            this.context = context;
        }
        public IEnumerable<User> GetUsers()
        {
            //return context.Users.Include(u=>u.Purchase).ThenInclude(p=>p.Product);
            return context.Users;
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        public int GetAuctionCount()
        {
            return context.Auctions.Count();
        }

        public decimal GetPurchaseCount(int year)
        {
            return  context.Purchases.Where(P => P.CreatedAt.Year == year).Sum(p=>p.Amount*p.ExtraAmount);
        }

    }
}
