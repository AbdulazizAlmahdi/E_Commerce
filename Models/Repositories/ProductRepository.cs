using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Models.Repositories
{
    public class ProductRepository : IUsersRepository<Product>
    {
        public readonly WebContext db;

        public ProductRepository(WebContext ctx)
        {
            db = ctx;
        }

        public void Add(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
        }


        public void Delete(Product product)
        {
           
            db.Products.Remove(product);
            db.SaveChanges();

           
        }

        public Product Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Product Find(int ID)
        {
            return db.Products.FirstOrDefault(p => p.Id == ID);
        }

        public IList<Product> List()
        {
            return this.db.Products.Include(u => u.Category).ToList();

        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
