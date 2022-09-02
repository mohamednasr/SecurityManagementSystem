using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class removeEquipmentValueFromSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentValue",
                table: "SiteEquipmentsAssign");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EquipmentValue",
                table: "SiteEquipmentsAssign",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
