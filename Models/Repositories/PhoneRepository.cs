using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class PhoneRepository : IRepository<Phone>
    {
        WebContext context;
        public PhoneRepository(WebContext db)
        {
            this.context = db;
        }
        public IQueryable<Phone> entities => context.Phones;

        public void Add(Phone entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
      
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Phone Find(string Text)
        {
            var phone = context.Phones.SingleOrDefault(a => a.Number == Text);

            return phone;
        }

        public Phone Find(int ID)
        {
            throw new NotImplementedException();
        }

        //public IList<User> List()
        //{
        //    return this.context.Users.Include(u => u.Place).Include(u => u.Phone).ToList();
        //}

        public void Update(Phone entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
