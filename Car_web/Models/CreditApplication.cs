using System.ComponentModel.DataAnnotations;

namespace Car_web.Models
{
    public class CreditApplication
    {
        [Key]
        public int Id { get; set; }

        // Step 1: Vehicle and Financing
        public string? ApplicationType { get; set; }
        public string? Relationship { get; set; }
        public string? VehicleId { get; set; }
        public string? PurchaseType { get; set; }
        public string? IntendedUse { get; set; }
        public string? Condition { get; set; }
        public int? Year { get; set; }
        public string? Make { get; set; }       
        public string? Model { get; set; }
        public string? Trim { get; set; }
        public string? StockNumber { get; set; }
        public string? VIN { get; set; }
        public int? PurchaseMonths { get; set; }
        public int? LeaseMilesPerYear { get; set; }
        public decimal? DownPayment { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public decimal? AmountToFinance { get; set; }
        public string? PreferredTerm { get; set; }
        public string? FinancingDownPayment { get; set; }
        public string? TradeYear { get; set; }
        public string? TradeMake { get; set; }
        public string? TradeModel { get; set; }
        public string? TradeMileage { get; set; }
        public string? TradeVIN { get; set; }
        public string? TradePayoffValue { get; set; }
        public string? TradeNotes { get; set; }
        public bool HasTrade { get; set; }

        // Step 2: Personal Info
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? SSN { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime? LicenseExpiry { get; set; }

        // Step 3: Residential Info
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? MonthlyRent { get; set; }
        public string? RentPayee { get; set; }
        public string? MothersMaidenName { get; set; }

        // Step 4: Additional Income
        public decimal? AdditionalIncomeAmount { get; set; }
        public string? IncomeSource { get; set; }

        // Add timestamps if needed
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
