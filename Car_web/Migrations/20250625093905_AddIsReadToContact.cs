using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReadToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "tbl_contact",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "tbl_contact");
        }
    }
}
