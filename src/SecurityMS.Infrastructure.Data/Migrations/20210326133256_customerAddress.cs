using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class customerAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ZoneId",
                table: "Customers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ZoneId",
                table: "Customers",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Zones_ZoneId",
                table: "Customers",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Zones_ZoneId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ZoneId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "Customers");
        }
    }
}
