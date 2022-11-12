using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class SalaryReportDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalariesReportDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SiteId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalariesReportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalariesReportDetails_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalariesReportEmployeesReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    SalaryReportId = table.Column<long>(type: "bigint", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Penalities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rewards = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancePaymentInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraDeductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalariesReportEmployeesReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalariesReportEmployeesReports_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportId",
                        column: x => x.SalaryReportId,
                        principalTable: "SalariesReportDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalariesReportDetails_SiteId",
                table: "SalariesReportDetails",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SalariesReportEmployeesReports_EmployeeId",
                table: "SalariesReportEmployeesReports",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalariesReportEmployeesReports_SalaryReportId",
                table: "SalariesReportEmployeesReports",
                column: "SalaryReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalariesReportEmployeesReports");

            migrationBuilder.DropTable(
                name: "SalariesReportDetails");
        }
    }
}
