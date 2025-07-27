using System.ComponentModel.DataAnnotations;

namespace Car_web.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        public string? Stock_id { get; set; }

        public string? Trim { get; set; }

        public string? Color { get; set; }

        public string? Vin {get; set; }
        public string? Engine { get; set; }

        public string? Exteriro_Color { get; set; }

        public string? Interior_Color { get; set; }
        public int? Seats { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public decimal? Price { get; set; }
        public int? Mileage { get; set; }
        public string? FuelType { get; set; }
        public string? Transmission { get; set; }
        public string? Description { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
