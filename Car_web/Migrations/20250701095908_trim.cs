using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class trim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "tbl_addcar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Exteriro_Color",
                table: "tbl_addcar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interior_Color",
                table: "tbl_addcar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trim",
                table: "tbl_addcar",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Engine",
                table: "tbl_addcar");

            migrationBuilder.DropColumn(
                name: "Exteriro_Color",
                table: "tbl_addcar");

            migrationBuilder.DropColumn(
                name: "Interior_Color",
                table: "tbl_addcar");

            migrationBuilder.DropColumn(
                name: "Trim",
                table: "tbl_addcar");
        }
    }
}
