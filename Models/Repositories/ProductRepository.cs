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
            context.Add(entity);
            context.SaveChanges();
        }
        // public void Delete(int id)
        // {
        //     var Product = Find(id);
        //   context.Products.Remove(Product);
        //    context.SaveChanges();
        // }

        void IRepository<Product>.Delete(int ID)
        {
            context.Products.Remove(Find(ID));
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product Find(int ID)
        {
            return context.Products.Include(u => u.Category).Include(u => u.ImagesProducts).FirstOrDefault(a=>a.Id==ID);
        }

        public Product Find(string Text)
        {
            throw new NotImplementedException();
        }
        public IQueryable<Product> show(int? ID, string name = "")
        {
            return GetChildProducts(1).AsQueryable();
        }
        List<Product> GetChildProducts(int userId)
        {
            List<Product> products = new List<Product>();
            foreach(var user in GetChild(userId)) {
            products.AddRange(context.Products.Where(p =>p.UserId == user.Id).Include(u => u.Category).Include(u => u.ImagesProducts).ToList());
            }

            return products;

        }
        List<User> GetChild(int id)
        {
            var users = context.Users.Where(x => x.UsersId == id || x.Id == id).ToList();

            var childUsers = users.AsEnumerable().Union(
                                        context.Users.AsEnumerable().Where(x => x.UsersId == id).SelectMany(y => GetChild(y.Id))).ToList();
            return childUsers;

        }
        public void Update(Product entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
