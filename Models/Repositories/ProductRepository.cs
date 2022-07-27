using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Models.Repositories
{

    public class ProductRepository : IRepository<Product>
    {
        WebContext context;



        public ProductRepository(WebContext db)
        {
            this.context = db;
        }
        public IQueryable<Product> Products => context.Products.Include(u => u.Category);

        public void Add(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
           
        }

        public Product Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Product Find(int ID)
        {
            return context.Products.Include(u => u.Category).FirstOrDefault(a=>a.Id==ID);
        }

        public Product Find(string Text)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories;
        }

        public IQueryable<Product> show(int? ID, string name = "")
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Product>.Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
