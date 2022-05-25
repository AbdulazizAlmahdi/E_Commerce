using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class UserRepository : IUsersRepository<User>
    {
        WebContext db=new WebContext();
        
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public User Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Find(int ID)
        {
            throw new NotImplementedException();
        }

        public IList<User> List()
        {
            return this.db.Users.Include(u=>u.Place).Include(u=>u.Phone).ToList();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
