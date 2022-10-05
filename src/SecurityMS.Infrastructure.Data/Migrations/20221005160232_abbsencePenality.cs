using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class abbsencePenality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Penality",
                table: "SiteEmployeesAttendance",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Penality",
                table: "SiteEmployeesAttendance");
        }
    }
}
