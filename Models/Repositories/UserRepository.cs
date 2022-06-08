using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class UserRepository : IRepository<User>
    {
        WebContext context;
        public UserRepository(WebContext db)
        {
            this.context = db;
        }
        public IQueryable<User> entities => context.Users.Include(u => u.Place).Include(u => u.Phone).Include(u => u.UserStatus);

        public void Add(User entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public User Find(int ID)
        {
            var user = context.Users.Include(u => u.Place).Include(u => u.Phone).SingleOrDefault(a => a.Id == ID);

            return user;
        }

        public User Find(string Text)
        {
            var user = context.Users.SingleOrDefault(a => a.Name == Text);

            return user;
        }

        //public IList<User> List()
        //{
        //    return this.context.Users.Include(u => u.Place).Include(u => u.Phone).ToList();
        //}

        public void Update(User entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
