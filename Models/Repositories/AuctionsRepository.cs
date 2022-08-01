using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class AuctionsRepository : IRepository<Auction>
    {
        WebContext context;
        public AuctionsRepository(WebContext db)
        {
            this.context = db;
        }
        public IQueryable<Auction> show(int? id,String name) => context.Auctions.Include(a=>a.Product);

        public void Add(Auction entity)
        {
                 throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public Auction Find(int ID)
        {
            throw new NotImplementedException();
        }

        public Auction Find(string Text)
        {
            throw new NotImplementedException();
        }

        public void Update(Auction entity)
        {
            throw new NotImplementedException();
        }

     
    }
}
