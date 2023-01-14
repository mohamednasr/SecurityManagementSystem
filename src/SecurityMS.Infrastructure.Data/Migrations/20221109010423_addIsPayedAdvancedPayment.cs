using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class addIsPayedAdvancedPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayedAt",
                table: "AdvancedPayments");

            migrationBuilder.DropColumn(
                name: "PayedBy",
                table: "AdvancedPayments");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "AdvancedPayments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "AdvancedPayments");

            migrationBuilder.AddColumn<DateTime>(
                name: "PayedAt",
                table: "AdvancedPayments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayedBy",
                table: "AdvancedPayments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
