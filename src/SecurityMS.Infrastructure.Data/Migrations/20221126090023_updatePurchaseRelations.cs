using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updatePurchaseRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId1",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_PurchaseId1",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PurchaseId1",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "PurchasesId",
                table: "PurchaseItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PurchaseId1",
                table: "PurchaseItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PurchasesId",
                table: "PurchaseItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_PurchaseId1",
                table: "PurchaseItems",
                column: "PurchaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId1",
                table: "PurchaseItems",
                column: "PurchaseId1",
                principalTable: "Purchases",
                principalColumn: "Id");
        }
    }
}
