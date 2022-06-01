using E_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.ViewModel
{
    public class UserViewModel
    {
        public User user { get; set; }
        public List<Place> places { get; set; }
    }
}
