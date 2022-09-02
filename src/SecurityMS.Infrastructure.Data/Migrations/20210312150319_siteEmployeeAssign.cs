using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class siteEmployeeAssign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteEquipments_EquipmentTypesLookup_EquipmentTypeId",
                table: "SiteEquipments");

            migrationBuilder.DropIndex(
                name: "IX_SiteEquipments_EquipmentTypeId",
                table: "SiteEquipments");

            migrationBuilder.DropColumn(
                name: "EquipmentTypeId",
                table: "SiteEquipments");

            migrationBuilder.AddColumn<long>(
                name: "EquipmentId",
                table: "SiteEquipments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CountriesLookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteEmployeesAssign",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteEmployeeId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<long>(nullable: false),
                    EmployeeShiftSalary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteEmployeesAssign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteEmployeesAssign_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SiteEmployeesAssign_SiteEmployees_SiteEmployeeId",
                        column: x => x.SiteEmployeeId,
                        principalTable: "SiteEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ManufactureId = table.Column<long>(nullable: false),
                    EquipmentTypeId = table.Column<long>(nullable: false),
                    EquipmentPrice = table.Column<decimal>(nullable: false),
                    EquipmentTotalCount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentTypesLookup_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypesLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipments_CountriesLookup_ManufactureId",
                        column: x => x.ManufactureId,
                        principalTable: "CountriesLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteEquipments_EquipmentId",
                table: "SiteEquipments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EquipmentTypeId",
                table: "Equipments",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ManufactureId",
                table: "Equipments",
                column: "ManufactureId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEmployeesAssign_EmployeeId",
                table: "SiteEmployeesAssign",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEmployeesAssign_SiteEmployeeId",
                table: "SiteEmployeesAssign",
                column: "SiteEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEquipments_Equipments_EquipmentId",
                table: "SiteEquipments",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteEquipments_Equipments_EquipmentId",
                table: "SiteEquipments");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "SiteEmployeesAssign");

            migrationBuilder.DropTable(
                name: "CountriesLookup");

            migrationBuilder.DropIndex(
                name: "IX_SiteEquipments_EquipmentId",
                table: "SiteEquipments");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "SiteEquipments");

            migrationBuilder.AddColumn<long>(
                name: "EquipmentTypeId",
                table: "SiteEquipments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SiteEquipments_EquipmentTypeId",
                table: "SiteEquipments",
                column: "EquipmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEquipments_EquipmentTypesLookup_EquipmentTypeId",
                table: "SiteEquipments",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypesLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
