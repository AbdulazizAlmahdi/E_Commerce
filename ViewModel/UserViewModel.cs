using E_commerce.Models;
using System.Collections.Generic;

namespace E_commerce.ViewModel
{
    public class UserViewModel
    {
        public User user { get; set; }
        public List<Place> places { get; set; }
        public List<Directorate> directorates { get; set; }
        public List<Governorate> governorates { get; set; }

    }
}
