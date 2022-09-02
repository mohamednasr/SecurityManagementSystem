using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updatesiteemployeesalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeShiftSalary",
                table: "SiteEmployeesAssign");

            migrationBuilder.AddColumn<decimal>(
                name: "EmployeeShiftSalary",
                table: "SiteEmployees",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeShiftSalary",
                table: "SiteEmployees");

            migrationBuilder.AddColumn<decimal>(
                name: "EmployeeShiftSalary",
                table: "SiteEmployeesAssign",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
