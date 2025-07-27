using System.ComponentModel.DataAnnotations;

namespace Car_web.Models
{
    public class TestDriveRequest
    {
        public int Id { get; set; }


        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PreferredDate { get; set; }

        [Required]
        public string PreferredTime { get; set; }

        [Required]
        public string StockNumber { get; set; }

        [Required]
        public string CarName { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
