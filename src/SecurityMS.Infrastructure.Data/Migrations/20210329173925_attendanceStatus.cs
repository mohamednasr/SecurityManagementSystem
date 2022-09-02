using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class attendanceStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeeAttendanceEntities_Employees_EmployeeId",
                table: "SiteEmployeeAttendanceEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeeAttendanceEntities_ShiftTypesLookup_ShiftId",
                table: "SiteEmployeeAttendanceEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeeAttendanceEntities_Sites_SiteId",
                table: "SiteEmployeeAttendanceEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteEmployeeAttendanceEntities",
                table: "SiteEmployeeAttendanceEntities");

            migrationBuilder.RenameTable(
                name: "SiteEmployeeAttendanceEntities",
                newName: "SiteEmployeesAttendance");

            migrationBuilder.RenameIndex(
                name: "IX_SiteEmployeeAttendanceEntities_SiteId",
                table: "SiteEmployeesAttendance",
                newName: "IX_SiteEmployeesAttendance_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_SiteEmployeeAttendanceEntities_ShiftId",
                table: "SiteEmployeesAttendance",
                newName: "IX_SiteEmployeesAttendance_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_SiteEmployeeAttendanceEntities_EmployeeId",
                table: "SiteEmployeesAttendance",
                newName: "IX_SiteEmployeesAttendance_EmployeeId");

            migrationBuilder.AddColumn<long>(
                name: "AttendanceStatusId",
                table: "SiteEmployeesAttendance",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteEmployeesAttendance",
                table: "SiteEmployeesAttendance",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AttendanceStatusLookup",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceStatusLookup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteEmployeesAttendance_AttendanceStatusId",
                table: "SiteEmployeesAttendance",
                column: "AttendanceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeesAttendance_AttendanceStatusLookup_AttendanceStatusId",
                table: "SiteEmployeesAttendance",
                column: "AttendanceStatusId",
                principalTable: "AttendanceStatusLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeesAttendance_Employees_EmployeeId",
                table: "SiteEmployeesAttendance",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeesAttendance_ShiftTypesLookup_ShiftId",
                table: "SiteEmployeesAttendance",
                column: "ShiftId",
                principalTable: "ShiftTypesLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeesAttendance_Sites_SiteId",
                table: "SiteEmployeesAttendance",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeesAttendance_AttendanceStatusLookup_AttendanceStatusId",
                table: "SiteEmployeesAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeesAttendance_Employees_EmployeeId",
                table: "SiteEmployeesAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeesAttendance_ShiftTypesLookup_ShiftId",
                table: "SiteEmployeesAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteEmployeesAttendance_Sites_SiteId",
                table: "SiteEmployeesAttendance");

            migrationBuilder.DropTable(
                name: "AttendanceStatusLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteEmployeesAttendance",
                table: "SiteEmployeesAttendance");

            migrationBuilder.DropIndex(
                name: "IX_SiteEmployeesAttendance_AttendanceStatusId",
                table: "SiteEmployeesAttendance");

            migrationBuilder.DropColumn(
                name: "AttendanceStatusId",
                table: "SiteEmployeesAttendance");

            migrationBuilder.RenameTable(
                name: "SiteEmployeesAttendance",
                newName: "SiteEmployeeAttendanceEntities");

            migrationBuilder.RenameIndex(
                name: "IX_SiteEmployeesAttendance_SiteId",
                table: "SiteEmployeeAttendanceEntities",
                newName: "IX_SiteEmployeeAttendanceEntities_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_SiteEmployeesAttendance_ShiftId",
                table: "SiteEmployeeAttendanceEntities",
                newName: "IX_SiteEmployeeAttendanceEntities_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_SiteEmployeesAttendance_EmployeeId",
                table: "SiteEmployeeAttendanceEntities",
                newName: "IX_SiteEmployeeAttendanceEntities_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteEmployeeAttendanceEntities",
                table: "SiteEmployeeAttendanceEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeeAttendanceEntities_Employees_EmployeeId",
                table: "SiteEmployeeAttendanceEntities",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeeAttendanceEntities_ShiftTypesLookup_ShiftId",
                table: "SiteEmployeeAttendanceEntities",
                column: "ShiftId",
                principalTable: "ShiftTypesLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteEmployeeAttendanceEntities_Sites_SiteId",
                table: "SiteEmployeeAttendanceEntities",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
