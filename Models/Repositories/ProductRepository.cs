using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace E_commerce.Models.Repositories
{
<<<<<<< HEAD
    public class ProductRepository : IProductRepository
=======
    public class ProductRepository : IProductRepository<Product>
>>>>>>> d91aedd9e2654f83d75f7f3ecd5e51d44d00eca5
    {
        public readonly WebContext db;


        
        public ProductRepository(WebContext ctx)
        {
            db = ctx;
        }

        public IQueryable<Product> Products => throw new NotImplementedException();

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

        public Product Find(int ID)
        {
            return db.Products.Include(u => u.Category).FirstOrDefault(a=>a.Id==ID);
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories;
        }

        public IEnumerable<Product> List()
        {
            return  db.Products.Include(u => u.Category);

        }

       

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
