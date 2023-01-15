using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class equipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteEquipments_Equipments_EquipmentId",
                table: "SiteEquipments");

            migrationBuilder.DropColumn(
                name: "EquipmentPrice",
                table: "Equipments");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SiteEmployeesAssign",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EquipmentDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<long>(nullable: false),
                    Serial = table.Column<string>(nullable: true),
                    ProductionDate = table.Column<DateTime>(nullable: false),
                    EquipmentPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentDetails_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SiteEquipmentsAssign",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteEquipmenteId = table.Column<long>(nullable: false),
                    EquipmentId = table.Column<long>(nullable: false),
                    EquipmentValue = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteEquipmentsAssign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteEquipmentsAssign_EquipmentDetails_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "EquipmentDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SiteEquipmentsAssign_SiteEquipments_SiteEquipmenteId",
                        column: x => x.SiteEquipmenteId,
                        principalTable: "SiteEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentDetails_EquipmentId",
                table: "EquipmentDetails",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEquipmentsAssign_EquipmentId",
                table: "SiteEquipmentsAssign",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEquipmentsAssign_SiteEquipmenteId",
                table: "SiteEquipmentsAssign",
                column: "SiteEquipmenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEquipments_Equipments_EquipmentId",
                table: "SiteEquipments",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteEquipments_Equipments_EquipmentId",
                table: "SiteEquipments");

            migrationBuilder.DropTable(
                name: "SiteEquipmentsAssign");

            migrationBuilder.DropTable(
                name: "EquipmentDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SiteEmployeesAssign");

            migrationBuilder.AddColumn<decimal>(
                name: "EquipmentPrice",
                table: "Equipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEquipments_Equipments_EquipmentId",
                table: "SiteEquipments",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
