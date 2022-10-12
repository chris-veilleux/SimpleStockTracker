using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStockTracker.Data.Migrations
{
    public partial class UpdateContributionLimitDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ContributionLimit",
                table: "Accounts",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ContributionLimit",
                table: "Accounts",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
