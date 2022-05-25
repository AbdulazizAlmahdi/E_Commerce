using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class UserStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
