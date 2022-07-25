using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{

    public class HelpRepository:IRepository<Help>{
        WebContext context;

        public HelpRepository(WebContext db)
        {
            this.context = db;
        }

        public void Add(Help entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Help Find(int ID)
        {
            return context.Helps.Include(h => h.Phone).SingleOrDefault(a => a.Id == ID);
        }

        public Help Find(string Text)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Help> show(int? _, string __="")       
        {
            return context.Helps.Include(h => h.Phone);
        }

        public void Update(Help entity)
        {
             context.Helps.Update(entity);
            context.SaveChanges();
        }
    }
}
