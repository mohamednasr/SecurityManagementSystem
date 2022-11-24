using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class AddExchangeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeTypesLookup",
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
                    table.PrimaryKey("PK_ExchangeTypesLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeEntity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyId = table.Column<long>(type: "bigint", nullable: false),
                    ExchangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExchangeTypeId = table.Column<int>(type: "int", nullable: false),
                    ExchangeTo = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeEntity_ExchangeTypesLookup_ExchangeTypeId",
                        column: x => x.ExchangeTypeId,
                        principalTable: "ExchangeTypesLookup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExchangeEntity_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExhangeItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    ItemQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhangeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExhangeItems_ExchangeEntity_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "ExchangeEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExhangeItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeEntity_ExchangeTypeId",
                table: "ExchangeEntity",
                column: "ExchangeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeEntity_SupplyId",
                table: "ExchangeEntity",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExhangeItems_ExchangeId",
                table: "ExhangeItems",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExhangeItems_ItemId",
                table: "ExhangeItems",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExhangeItems");

            migrationBuilder.DropTable(
                name: "ExchangeEntity");

            migrationBuilder.DropTable(
                name: "ExchangeTypesLookup");
        }
    }
}
