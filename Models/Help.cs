using System;
using System.Collections.Generic;

#nullable disable

namespace E_commerce.Models
{
    public partial class Help
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public int? PhoneId { get; set; }

        public virtual Phone Phone { get; set; }
    }
}
