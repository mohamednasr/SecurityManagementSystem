using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class employeeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileNumber",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeBirthCertificate",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeCriminalCertificate",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeEducationCertificate",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeIDCopy",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeMilitaryCertificate",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludePersonalPhotos",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeWorkStub",
                table: "Employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludeBirthCertificate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludeCriminalCertificate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludeEducationCertificate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludeIDCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludeMilitaryCertificate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludePersonalPhotos",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsIncludeWorkStub",
                table: "Employees");
        }
    }
}
