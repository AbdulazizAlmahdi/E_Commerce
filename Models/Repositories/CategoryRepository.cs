using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        public readonly WebContext context;



        public CategoryRepository(WebContext context)
        {
            this.context = context;
        }
        public void Add(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
            
        }

        public Category Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Category Find(int ID)
        {
            throw new NotImplementedException();
        }

        public Category Find(string Text)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> List()
        {
            return context.Categories;
        }

        public IQueryable<Category> show(int? ID, string name = "")
        {
            return context.Categories;
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<Category>.Delete(int ID)
        {
            throw new NotImplementedException();
        }
    }

    
}
