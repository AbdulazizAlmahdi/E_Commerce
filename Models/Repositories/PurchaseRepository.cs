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
              var purchase = Find(ID);
          context.Purchases.Remove(purchase);
           context.SaveChanges();
        }

        public Purchase Find(int ID)
        {
            return context.Purchases.Include(p=>p.Products).ThenInclude(p=>p.ImagesProducts).SingleOrDefault(a => a.Id == ID);
        }

        public Purchase Find(string Text)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Purchase> show(int? _, string __="")       
        {
            return context.Purchases.Include(h => h.Products).Include(h => h.User);
        }
        ICollection<Product> GetProducts(int PurchaseId)
        {
            return context.Products.Where(p => p.PurchaseId == PurchaseId).ToList();
        }
        public void Update(Purchase entity)
        {
            ICollection<Product> products = GetProducts(entity.Id);
            UpdateProduct(products);
            context.Purchases.Update(entity);
            context.SaveChanges();

            //UpdateProduct(Comparison(oldProducts, entity.Products).ToList());
        }
         public void UpdateProduct(ICollection<Product> entities)
        {
            foreach (var item in entities)
            {
                item.PurchaseId = null;
                context.Update(item);
                context.SaveChanges();
            }
        }
    }
}
