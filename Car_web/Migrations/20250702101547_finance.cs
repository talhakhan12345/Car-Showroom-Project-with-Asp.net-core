using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class finance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
