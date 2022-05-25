using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class ImagesUser
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
