using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class updateCustomerContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerContacts_Contracts_ContractsEntityId",
                table: "CustomerContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_ContractsEntityId",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "ContractsEntityId",
                table: "CustomerContacts");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractContactPersonId",
                table: "Contracts",
                column: "ContractContactPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_CustomerContacts_ContractContactPersonId",
                table: "Contracts",
                column: "ContractContactPersonId",
                principalTable: "CustomerContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_CustomerContacts_ContractContactPersonId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractContactPersonId",
                table: "Contracts");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Zones",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UniformDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Uniform",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Sites",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SiteEquipmentsAssign",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SiteEquipments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SiteEmployeesAttendance",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SiteEmployeesAssign",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "SiteEmployees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ShiftTypesLookup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Rewards",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Penalties",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Jobs",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Items",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ItemDetail",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "InvoicesDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Invoices",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Government",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "EquipmentTypesLookup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Equipments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "EquipmentDetails",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EndServiceReasonLookup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Employees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Departments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CustomerTypeLookup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Customers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CustomerContacts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "ContractsEntityId",
                table: "CustomerContacts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CountriesLookup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "BlackListEntity",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AttendanceStatusLookup",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUserClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoleClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AdvancedPayments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContractsEntityId",
                table: "CustomerContacts",
                column: "ContractsEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerContacts_Contracts_ContractsEntityId",
                table: "CustomerContacts",
                column: "ContractsEntityId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
