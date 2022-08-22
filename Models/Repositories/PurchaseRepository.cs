using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{

    public class PurchaseRepository : IRepository<Purchase>
    {
        WebContext context;

        public PurchaseRepository(WebContext db)
        {
            this.context = db;
        }

        public void Add(Purchase entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Purchase Find(int ID)
        {
            return context.Purchases.SingleOrDefault(a => a.Id == ID);
        }

        public Purchase Find(string Text)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Purchase> show(int? _, string __="")       
        {
            return context.Purchases.Include(h => h.Products).Include(h => h.User);
        }

        public void Update(Purchase entity)
        {
             context.Purchases.Update(entity);
            context.SaveChanges();
        }
    }
}
