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
        public IQueryable<User> show(int? ID,String name= "")
        {
            return GetChild(ID??0, name).AsQueryable();
        }
        List<User> GetChild(int id,String name="")
        {
            var users = context.Users.Where(x => (x.UsersId == id) &&x.Name.Contains(name)).Include(u => u.Place).Include(u => u.Phone).ToList();

            var childUsers = users.AsEnumerable().Union(
                                        context.Users.AsEnumerable().Where(x => x.UsersId == id).SelectMany(y => GetChild(y.Id,name))).ToList();
            return childUsers;

        }

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
            var user = context.Users.Include(u => u.Place).Include(u => u.Phone).Include(u => u.Users).SingleOrDefault(a => a.Id == ID);
            return user;
        }

        public User Find(string Text)
        {
            var user = context.Users.SingleOrDefault(a => a.Name == Text);

            return user;
        }

        public void Update(User entity)
        {
            context.Users.Update(entity);
            context.SaveChanges();          
        }
    }
}
