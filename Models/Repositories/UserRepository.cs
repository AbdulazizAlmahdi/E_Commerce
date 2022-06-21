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
        //public IQueryable<User> show(int? ID) => context.Users.Where(u=>u.UsersId == 5||u.Id==5).SelectMany(x => x.InverseUsers).Include(u => u.Place).Include(u => u.Phone).Include(u => u.UserStatus);
        public IQueryable<User> show(int? ID)
        {
            return GetChild(5).AsQueryable();
        }
        List<User> GetChild(int id)
        {

            var users = context.Users.Where(x => x.UsersId == id || x.Id == id).Include(u => u.Place).Include(u => u.Phone).Include(u => u.UserStatus).ToList();

            var childUsers = users.AsEnumerable().Union(
                                        context.Users.AsEnumerable().Where(x => x.UsersId == id).SelectMany(y => GetChild(y.Id))).ToList();
            return childUsers;

            //var users = context.Users.Where(x => x.UsersId == id).Include(u => u.Place).Include(u => u.Phone).Include(u => u.UserStatus).ToList();
            //List<User> temp = new List<User>();
            //if (users.Count == 0) return users;
            //foreach (var user in users)
            //{
            //    var x = GetChild(user.Id);
            //    if (x.Count != 0)
            //        temp.AddRange(x);

            //}
            //return temp;

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
