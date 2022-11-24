using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateSalariesRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportDetailsId",
            //    table: "SalariesReportEmployeesReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportId",
                table: "SalariesReportEmployeesReports");

            //migrationBuilder.DropIndex(
            //    name: "IX_SalariesReportEmployeesReports_SalaryReportDetailsId",
            //    table: "SalariesReportEmployeesReports");

            //migrationBuilder.DropColumn(
            //    name: "SalaryReportDetailsId",
            //    table: "SalariesReportEmployeesReports");

            migrationBuilder.AddForeignKey(
                name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportId",
                table: "SalariesReportEmployeesReports",
                column: "SalaryReportId",
                principalTable: "SalariesReportDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportId",
                table: "SalariesReportEmployeesReports");

            //migrationBuilder.AddColumn<long>(
            //    name: "SalaryReportDetailsId",
            //    table: "SalariesReportEmployeesReports",
            //    type: "bigint",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_SalariesReportEmployeesReports_SalaryReportDetailsId",
            //    table: "SalariesReportEmployeesReports",
            //    column: "SalaryReportDetailsId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportDetailsId",
            //    table: "SalariesReportEmployeesReports",
            //    column: "SalaryReportDetailsId",
            //    principalTable: "SalariesReportDetails",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalariesReportEmployeesReports_SalariesReportDetails_SalaryReportId",
                table: "SalariesReportEmployeesReports",
                column: "SalaryReportId",
                principalTable: "SalariesReportDetails",
                principalColumn: "Id");
        }
    }
}
