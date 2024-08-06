using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace E_commerce.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titel { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [AllowNull]
        public virtual int? UserId { get; set; }
        public virtual User user { get; set; }

    }
}
