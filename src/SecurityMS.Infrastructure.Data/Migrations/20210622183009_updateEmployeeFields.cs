using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateEmployeeFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludePersonalPhotos",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludeMilitaryCertificate",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludeEducationCertificate",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludeBirthCertificate",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "BirthCertificateCopy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CriminalCertificateSoftCopy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationCertificateSoftCopy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDSoftCopy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MilitaryCertificateCopy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalPhotoSoftCopy",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkStubSoftCopy",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthCertificateCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CriminalCertificateSoftCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EducationCertificateSoftCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IDSoftCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MilitaryCertificateCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonalPhotoSoftCopy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkStubSoftCopy",
                table: "Employees");

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludePersonalPhotos",
                table: "Employees",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludeMilitaryCertificate",
                table: "Employees",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludeEducationCertificate",
                table: "Employees",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsIncludeBirthCertificate",
                table: "Employees",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
