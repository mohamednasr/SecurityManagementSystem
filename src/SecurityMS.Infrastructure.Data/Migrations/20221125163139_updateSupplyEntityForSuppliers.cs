using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateSupplyEntityForSuppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_BankTransactions_BankId",
            //    table: "BankTransactions");

            migrationBuilder.AddColumn<long>(
                name: "SuppliedFromId",
                table: "Supplies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SuppliedFromName",
                table: "Supplies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierTypeId",
                table: "Supplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SuppliersTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliersTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_SupplierTypeId",
                table: "Supplies",
                column: "SupplierTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_SuppliersTypes_SupplierTypeId",
                table: "Supplies",
                column: "SupplierTypeId",
                principalTable: "SuppliersTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_SuppliersTypes_SupplierTypeId",
                table: "Supplies");

            migrationBuilder.DropTable(
                name: "SuppliersTypes");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_SupplierTypeId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "SuppliedFromId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "SuppliedFromName",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "SupplierTypeId",
                table: "Supplies");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BankTransactions_BankId",
            //    table: "BankTransactions",
            //    column: "BankId");
        }
    }
}
