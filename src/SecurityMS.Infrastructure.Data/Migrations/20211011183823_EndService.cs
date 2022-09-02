using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class EndService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EndServiceReasonId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EndServiceReasonLookup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndServiceReasonLookup", x => x.Id);
                });
            var addLookupScript = @"INSERT into [dbo].[EndServiceReasonLookup] ([Name]) Values (N'إستقاله'), (N'إنهاء تعاقد'), (N'إقاله')";

            migrationBuilder.Sql(addLookupScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndServiceReasonLookup");

            migrationBuilder.DropColumn(
                name: "EndServiceReasonId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Employees");
        }
    }
}
