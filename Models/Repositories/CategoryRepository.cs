using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class CategoryRepository : IUsersRepository<Category>
    {
        public readonly WebContext db;



        public CategoryRepository(WebContext ctx)
        {
            db = ctx;
        }
        public void Add(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
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

        public IList<Category> List()
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
