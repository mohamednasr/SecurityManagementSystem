using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class ItemsAndUniforms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Equipments");

            migrationBuilder.AddColumn<long>(
                name: "AvailableTotalCount",
                table: "Equipments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Equipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumAlert",
                table: "Equipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceOutDate",
                table: "EquipmentDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "EquipmentDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TotalCount = table.Column<int>(nullable: false),
                    AvailableTotalCount = table.Column<int>(nullable: false),
                    MinimumAlert = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uniform",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    TotalCount = table.Column<int>(nullable: false),
                    AvailableTotalCount = table.Column<int>(nullable: false),
                    MinimumAlert = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uniform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<long>(nullable: false),
                    AssignedTo = table.Column<long>(nullable: true),
                    AssignedToType = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    OutDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDetail_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UniformDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniformId = table.Column<long>(nullable: false),
                    AssignedTo = table.Column<long>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    OutDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniformDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniformDetails_Employees_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UniformDetails_Uniform_UniformId",
                        column: x => x.UniformId,
                        principalTable: "Uniform",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetail_ItemId",
                table: "ItemDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UniformDetails_AssignedTo",
                table: "UniformDetails",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_UniformDetails_UniformId",
                table: "UniformDetails",
                column: "UniformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDetail");

            migrationBuilder.DropTable(
                name: "UniformDetails");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Uniform");

            migrationBuilder.DropColumn(
                name: "AvailableTotalCount",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "MinimumAlert",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ServiceOutDate",
                table: "EquipmentDetails");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "EquipmentDetails");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Equipments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
