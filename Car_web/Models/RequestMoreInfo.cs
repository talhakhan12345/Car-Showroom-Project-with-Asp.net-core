using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_web.Models
{
    [Table("tbl_moreinfo")]
    public class RequestMoreInfo
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public bool PreferCall { get; set; }

        public bool PreferSMS { get; set; }

        public bool PreferEmail { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
