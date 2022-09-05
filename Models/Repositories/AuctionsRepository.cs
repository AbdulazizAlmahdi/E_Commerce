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
        public IQueryable<Auction> show(int? id,String name) =>
            context.Auctions.Include(a=>a.Product).Include(a=>a.AuctionsUsers).ThenInclude(u=>u.User).ThenInclude(u=>u.Phone);

        public void Add(Auction entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int ID)
        {
            var auction = Find(ID);
            context.Auctions.Remove(auction);
            context.SaveChanges();
        }

        public Auction Find(int ID)
        {
            var auction = context.Auctions.Include(a => a.Product).SingleOrDefault(a => a.Id == ID);
            return auction;
        }

        public Auction Find(string Text)
        {
            throw new NotImplementedException();
        }

        public void Update(Auction entity)
        {
            context.Auctions.Update(entity);
            context.SaveChanges();
        }

     
    }
}
