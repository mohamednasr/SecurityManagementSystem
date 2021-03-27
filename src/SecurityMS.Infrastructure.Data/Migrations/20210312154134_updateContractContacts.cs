using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateContractContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContractContactPersonId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractContactPersonId",
                table: "Contracts",
                column: "ContractContactPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_CustomerContacts_ContractContactPersonId",
                table: "Contracts",
                column: "ContractContactPersonId",
                principalTable: "CustomerContacts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CustomerContacts_ContractContactPersonId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractContactPersonId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractContactPersonId",
                table: "Contracts");
        }
    }
}
