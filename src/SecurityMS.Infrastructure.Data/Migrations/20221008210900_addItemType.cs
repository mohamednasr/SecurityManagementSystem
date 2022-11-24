using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class addItemType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_TypeId",
                table: "Items",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SupplyTypes_TypeId",
                table: "Items",
                column: "TypeId",
                principalTable: "SupplyTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_SupplyTypes_TypeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_TypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Items");
        }
    }
}
