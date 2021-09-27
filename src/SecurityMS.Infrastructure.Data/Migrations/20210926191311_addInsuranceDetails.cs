using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class addInsuranceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InsuranceAmount",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceEndDate",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePercentage",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceStartDate",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceAmount",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsuranceEndDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsurancePercentage",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "InsuranceStartDate",
                table: "Employees");
        }
    }
}
