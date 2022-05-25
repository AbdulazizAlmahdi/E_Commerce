using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
