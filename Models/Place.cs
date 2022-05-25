using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Place
    {
        public Place()
        {
            InversePlaceNavigation = new HashSet<Place>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? PlaceId { get; set; }

        public virtual Place PlaceNavigation { get; set; }
        public virtual ICollection<Place> InversePlaceNavigation { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
