using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class fixContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CustomerContacts_ContractContactPersonId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractContactPersonId",
                table: "Contracts");

            migrationBuilder.AddColumn<long>(
                name: "ContractsEntityId",
                table: "CustomerContacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContractsEntityId",
                table: "CustomerContacts",
                column: "ContractsEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_Contracts_ContractsEntityId",
                table: "CustomerContacts",
                column: "ContractsEntityId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_Contracts_ContractsEntityId",
                table: "CustomerContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_ContractsEntityId",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "ContractsEntityId",
                table: "CustomerContacts");

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
    }
}
