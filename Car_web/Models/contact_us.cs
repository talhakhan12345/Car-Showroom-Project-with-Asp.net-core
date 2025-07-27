using System.ComponentModel.DataAnnotations;

namespace Car_web.Models
{
    public class contact_us
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public string PreferredReplyMethods { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
