using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models.Repositories
{
    public interface IAnalyticsRepository
    {
        public IEnumerable<User> GetUsers();
        public IEnumerable<Product> GetProducts();
        
    }
}
