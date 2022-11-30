using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class setUniquePurchaseCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "Supplies",
                newName: "PurchaseCode");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseCode",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseCode",
                table: "Purchases",
                column: "PurchaseCode",
                unique: true,
                filter: "[PurchaseCode] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Purchases_PurchaseCode",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "PurchaseCode",
                table: "Supplies",
                newName: "PurchaseId");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseCode",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
