using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateSupplierQuantityAndPurchaseCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseCode",
                table: "Supplies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuppliedQuantity",
                table: "PurchaseItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuppliedQuantity",
                table: "PurchaseItems");

            migrationBuilder.AlterColumn<long>(
                name: "PurchaseCode",
                table: "Supplies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
