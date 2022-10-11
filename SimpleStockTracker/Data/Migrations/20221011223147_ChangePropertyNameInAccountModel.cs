using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStockTracker.Data.Migrations
{
    public partial class ChangePropertyNameInAccountModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Accounts",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Accounts",
                newName: "Nickname");
        }
    }
}
