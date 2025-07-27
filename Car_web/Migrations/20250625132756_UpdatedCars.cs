using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "tbl_addcar");

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "tbl_addcar",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seats",
                table: "tbl_addcar");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "tbl_addcar",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
