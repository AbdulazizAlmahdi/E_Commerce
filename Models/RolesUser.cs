
#nullable disable

namespace E_commerce.Models
{
    public partial class RolesUser
    {
        public int RolesId { get; set; }
        public int UsersId { get; set; }

        public virtual Role Roles { get; set; }
        public virtual User Users { get; set; }
    }
}
