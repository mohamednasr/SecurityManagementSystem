using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class IncomeTaxesMatrix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fees",
                table: "SalariesReportEmployeesReports",
                newName: "Taxes");

            migrationBuilder.AddColumn<decimal>(
                name: "Insurance",
                table: "SalariesReportEmployeesReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "IncomeTaxesMatrix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RangeFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RangeTo = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxesPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxesExemption = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTaxesMatrix", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeTaxesMatrix");

            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "SalariesReportEmployeesReports");

            migrationBuilder.RenameColumn(
                name: "Taxes",
                table: "SalariesReportEmployeesReports",
                newName: "Fees");
        }
    }
}
