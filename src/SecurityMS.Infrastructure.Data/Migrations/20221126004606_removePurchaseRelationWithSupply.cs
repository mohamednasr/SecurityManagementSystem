using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class removePurchaseRelationWithSupply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Purchases_PurchaseId",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_PurchaseId",
                table: "Supplies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Supplies_PurchaseId",
                table: "Supplies",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Purchases_PurchaseId",
                table: "Supplies",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");
        }
    }
}
