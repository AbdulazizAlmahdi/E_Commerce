using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class ImagesProduct
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
