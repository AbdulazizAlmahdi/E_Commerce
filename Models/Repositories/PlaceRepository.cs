using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class PlaceRepository : IRepository<Place>
    {
        WebContext context;
        public PlaceRepository(WebContext db)
        {
            this.context = db;
        }
        public IQueryable<Place> entities => context.Places;

        public void Add(Place entity)
        {
                 throw new NotImplementedException();
        }

        public Place Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(Place entity)
        {
            throw new NotImplementedException();
        }

        public Place Find(int ID)
        {
            throw new NotImplementedException();
        }

        public Place Find(string Text)
        {
            throw new NotImplementedException();
        }

        public void Update(Place entity)
        {
            throw new NotImplementedException();
        }
    }
}
