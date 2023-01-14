using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class renameEntitesForSalaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeShiftSalary",
                table: "SiteEmployeesAssign",
                newName: "EmployeeSalary");

            migrationBuilder.RenameColumn(
                name: "ShiftValue",
                table: "SiteEmployees",
                newName: "EmployeeSalary");

            migrationBuilder.RenameColumn(
                name: "EmployeeShiftSalary",
                table: "SiteEmployees",
                newName: "EmployeeCost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeSalary",
                table: "SiteEmployeesAssign",
                newName: "EmployeeShiftSalary");

            migrationBuilder.RenameColumn(
                name: "EmployeeSalary",
                table: "SiteEmployees",
                newName: "ShiftValue");

            migrationBuilder.RenameColumn(
                name: "EmployeeCost",
                table: "SiteEmployees",
                newName: "EmployeeShiftSalary");
        }
    }
}
