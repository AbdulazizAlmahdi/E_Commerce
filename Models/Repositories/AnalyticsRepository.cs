using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        public readonly WebContext context;



        public AnalyticsRepository(WebContext context)
        {
            this.context = context;
        }
        public IEnumerable<User> GetUsers()
        {
            return context.Users.Include(u=>u.Purchase).ThenInclude(p=>p.Product);
        }
    }
}
