using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Role
    {
        public Role()
        {
            RolesUsers = new HashSet<RolesUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? PermissionsId { get; set; }

        public virtual Permission Permissions { get; set; }
        public virtual ICollection<RolesUser> RolesUsers { get; set; }
    }
}
