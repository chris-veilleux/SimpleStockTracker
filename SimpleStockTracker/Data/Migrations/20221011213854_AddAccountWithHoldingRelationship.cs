using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleStockTracker.Data.Migrations
{
    public partial class AddAccountWithHoldingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Account",
                table: "Holding",
                newName: "AccountId");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributionLimit = table.Column<float>(type: "real", nullable: false),
                    OpeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountHolder = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holding_AccountId",
                table: "Holding",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holding_Accounts_AccountId",
                table: "Holding",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holding_Accounts_AccountId",
                table: "Holding");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Holding_AccountId",
                table: "Holding");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Holding",
                newName: "Account");
        }
    }
}
