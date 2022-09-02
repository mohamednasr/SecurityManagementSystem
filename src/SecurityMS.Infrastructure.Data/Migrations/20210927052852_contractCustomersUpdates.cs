using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class contractCustomersUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxFileNumber",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CommercialProfits",
                table: "Contracts",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ContractPDF",
                table: "Contracts",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxPercentage",
                table: "Contracts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxFileNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CommercialProfits",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ContractPDF",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "Contracts");
        }
    }
}
