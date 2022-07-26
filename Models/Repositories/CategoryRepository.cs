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
        public Category Find(int ID)
        {
            return context.Categories.Include(c=>c.categories).SingleOrDefault(c => c.Id == ID);
        }

        public Category Find(string Text)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> show(int? ID, string name = "")
        {
            return context.Categories.Include(c=>c.categories);
        }

        public void Update(Category entity)
        {
          context.Categories.Update(entity);
            context.SaveChanges();
        }

        void IRepository<Category>.Delete(int ID)
        {
            context.Categories.Remove(Find(ID));
            context.SaveChanges();

        }
    }

    
}
