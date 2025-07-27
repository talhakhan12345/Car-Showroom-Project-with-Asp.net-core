using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class AddPreferredReplyMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredReplyMethods",
                table: "tbl_contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredReplyMethods",
                table: "tbl_contact");
        }
    }
}
