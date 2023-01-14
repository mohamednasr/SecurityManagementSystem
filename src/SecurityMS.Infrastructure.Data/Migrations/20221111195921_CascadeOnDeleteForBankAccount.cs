using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class CascadeOnDeleteForBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankId",
                table: "BankTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankId",
                table: "BankTransactions",
                column: "BankId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankId",
                table: "BankTransactions");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankId",
                table: "BankTransactions",
                column: "BankId",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }
    }
}
