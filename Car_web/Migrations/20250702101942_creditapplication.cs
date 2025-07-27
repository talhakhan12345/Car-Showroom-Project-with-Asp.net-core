using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class creditapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_us",
                table: "contact_us");

            migrationBuilder.RenameTable(
                name: "contact_us",
                newName: "tbl_contact");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_contact",
                table: "tbl_contact",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tbl_finance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    PurchaseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntendedUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseMonths = table.Column<int>(type: "int", nullable: true),
                    LeaseMilesPerYear = table.Column<int>(type: "int", nullable: true),
                    DownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MonthlyPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AmountToFinance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PreferredTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancingDownPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeMake = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeMileage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeVIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradePayoffValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TradeNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasTrade = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyRent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentPayee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersMaidenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalIncomeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IncomeSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_finance", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_finance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_contact",
                table: "tbl_contact");

            migrationBuilder.RenameTable(
                name: "tbl_contact",
                newName: "contact_us");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_us",
                table: "contact_us",
                column: "Id");
        }
    }
}
