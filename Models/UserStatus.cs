using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace E_commerce.Models
{
    public partial class UserStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<User> Users { get; set; }
    }
}
