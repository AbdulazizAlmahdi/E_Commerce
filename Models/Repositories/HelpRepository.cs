using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{

    public class HelpRepository:IHelpRepository{
        WebContext context;

        public HelpRepository(WebContext db)
        {
            this.context = db;
        }

        public void Add(Help entity)
        {
            context.Helps.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Help help)
        {
            context.Helps.Remove(help);
            context.SaveChanges();

        }

        public Help Find(int ID)
        {
            return context.Helps.FirstOrDefault(a => a.Id == ID);
        }

        public IEnumerable<Help> GetAllHelpRequests()
        {
            return context.Helps.Include(h => h.Phone);
        }

        public Help Update(Help entity) 
        {
            throw new NotImplementedException();
        }


    
    }
}
