using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class PenaltiesDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PenalityDate",
                table: "Penalties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PenalityDate",
                table: "Penalties");
        }
    }
}
