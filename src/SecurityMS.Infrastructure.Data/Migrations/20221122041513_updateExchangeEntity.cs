using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateExchangeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeEntity_Supplies_SupplyId",
                table: "ExchangeEntity");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeEntity_SupplyId",
                table: "ExchangeEntity");

            migrationBuilder.DropColumn(
                name: "SupplyId",
                table: "ExchangeEntity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SupplyId",
                table: "ExchangeEntity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeEntity_SupplyId",
                table: "ExchangeEntity",
                column: "SupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeEntity_Supplies_SupplyId",
                table: "ExchangeEntity",
                column: "SupplyId",
                principalTable: "Supplies",
                principalColumn: "Id");
        }
    }
}
