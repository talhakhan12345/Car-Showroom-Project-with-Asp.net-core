using System.ComponentModel.DataAnnotations;

namespace Car_web.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string VehicleInfo { get; set; }
        public string Mileage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        public bool ContactCall { get; set; }
        public bool ContactSMS { get; set; }
        public bool ContactEmail { get; set; }
        public bool Consent { get; set; }
    }
}
