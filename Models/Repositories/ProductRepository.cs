using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Models.Repositories
{
    public class ProductRepository : IProductRepository<Product>
    {
        public readonly WebContext db;

        public ProductRepository(WebContext ctx)
        {
            db = ctx;
        }

        public IQueryable<Product> Products => throw new NotImplementedException();

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Product Find(int ID)
        {
            throw new NotImplementedException();
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
