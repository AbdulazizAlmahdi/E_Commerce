using E_commerce.Models;
using System.Collections.Generic;

namespace E_commerce.ViewModel
{
    public class UserInfoViewModel
    {
        public IEnumerable<Product> GetProducts { get; set; }
    }
}
