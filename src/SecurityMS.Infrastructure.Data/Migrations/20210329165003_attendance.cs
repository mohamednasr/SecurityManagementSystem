using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class attendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteEmployeeAttendanceEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<long>(nullable: false),
                    SiteId = table.Column<long>(nullable: false),
                    AttendanceDate = table.Column<DateTime>(nullable: false),
                    ShiftId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteEmployeeAttendanceEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteEmployeeAttendanceEntities_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteEmployeeAttendanceEntities_ShiftTypesLookup_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "ShiftTypesLookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteEmployeeAttendanceEntities_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteEmployeeAttendanceEntities_EmployeeId",
                table: "SiteEmployeeAttendanceEntities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEmployeeAttendanceEntities_ShiftId",
                table: "SiteEmployeeAttendanceEntities",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteEmployeeAttendanceEntities_SiteId",
                table: "SiteEmployeeAttendanceEntities",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteEmployeeAttendanceEntities");
        }
    }
}
