using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
