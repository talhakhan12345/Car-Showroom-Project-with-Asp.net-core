using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_web.Migrations
{
    /// <inheritdoc />
    public partial class rrrrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_trade",
                table: "tbl_trade");

            migrationBuilder.RenameTable(
                name: "tbl_trade",
                newName: "tbl_req");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_req",
                table: "tbl_req",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_req",
                table: "tbl_req");

            migrationBuilder.RenameTable(
                name: "tbl_req",
                newName: "tbl_trade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_trade",
                table: "tbl_trade",
                column: "Id");
        }
    }
}
