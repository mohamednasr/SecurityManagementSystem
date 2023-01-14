using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class PermissionTypesLookUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "TreasuryWithdrawPermission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BenificiaryCode",
                table: "TreasuryWithdrawPermission",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Listed",
                table: "TreasuryWithdrawPermission",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "TreasuryDepositPermission",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BenificiaryCode",
                table: "TreasuryDepositPermission",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetsLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsLookup", x => x.Id);
                });



            migrationBuilder.CreateTable(
                name: "ExpensesLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreasuryDepositPermissionTypesLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreasuryDepositPermissionTypesLookup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreasuryWithdrawPermissionTypesLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreasuryWithdrawPermissionTypesLookup", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AssetsLookup",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, null, false, "أثاث", null, null },
                    { 2, null, null, false, "أجهزة كمبيوتر و نظم حسابيه", null, null },
                    { 3, null, null, false, "اجهزة كهربائية", null, null },
                    { 4, null, null, false, "تشطيبات و ديكورات", null, null },
                    { 5, null, null, false, "أجهزة تكييف", null, null },
                    { 6, null, null, false, "وسائل نقل و انتقال", null, null },
                    { 7, null, null, false, "أسلحة", null, null },
                    { 8, null, null, false, "أجهزة لاسلكي", null, null }
                });

            migrationBuilder.InsertData(
                table: "ExpensesLookup",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, null, false, "بوفيه و ضيافة", null, null },
                    { 2, null, null, false, "اجور و مرتبات", null, null },
                    { 3, null, null, false, "م.بنكية", null, null },
                    { 4, null, null, false, "رسوم", null, null },
                    { 5, null, null, false, "نثريات", null, null },
                    { 6, null, null, false, "انتقالات", null, null },
                    { 7, null, null, false, "اعانة وفاة", null, null },
                    { 8, null, null, false, "اصلاح و صيانة", null, null },
                    { 9, null, null, false, "تليفونات", null, null },
                    { 10, null, null, false, "ادوات مكتبية", null, null },
                    { 11, null, null, false, "ايجار", null, null },
                    { 12, null, null, false, "اكراميات", null, null },
                    { 13, null, null, false, "اتعاب مهنية", null, null },
                    { 14, null, null, false, "تامينات اجتماعية", null, null },
                    { 15, null, null, false, "تامينات مقاولات", null, null },
                    { 16, null, null, false, "تحصيل عملاء", null, null },
                    { 17, null, null, false, "نت مقر الشركة", null, null },
                    { 18, null, null, false, "ادوات كهربائية", null, null },
                    { 19, null, null, false, "بنزين", null, null },
                    { 20, null, null, false, "غاز و كهرباءومياه", null, null },
                    { 21, null, null, false, "غرامات", null, null },
                    { 22, null, null, false, "غرامات موقع", null, null },
                    { 23, null, null, false, "مساهمة تكافلية", null, null },
                    { 24, null, null, false, "ايجار سيارات", null, null },
                    { 25, null, null, false, "غسيل و مكوى", null, null },
                    { 26, null, null, false, "زي امن", null, null },
                    { 27, null, null, false, "وثائق تامين", null, null },
                    { 28, null, null, false, "تبرعات", null, null },
                    { 29, null, null, false, "علاج", null, null }
                });

            migrationBuilder.InsertData(
                table: "TreasuryDepositPermissionTypesLookup",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, null, false, "جاري شريك", null, null },
                    { 2, null, null, false, "عهد", null, null },
                    { 3, null, null, false, "بنك", null, null },
                    { 4, null, null, false, "سلف", null, null },
                    { 5, null, null, false, "عملاء", null, null }
                });

            migrationBuilder.InsertData(
                table: "TreasuryWithdrawPermissionTypesLookup",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, null, false, "مصاريف تشغيل", null, null },
                    { 2, null, null, false, "مصاريف عمومية", null, null },
                    { 3, null, null, false, "موردين", null, null },
                    { 4, null, null, false, "الاصول", null, null },
                    { 5, null, null, false, "العهد", null, null },
                    { 6, null, null, false, "سلف", null, null },
                    { 7, null, null, false, "جاري شريك", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreasuryWithdrawPermission_TypeId",
                table: "TreasuryWithdrawPermission",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TreasuryDepositPermission_TypeId",
                table: "TreasuryDepositPermission",
                column: "TypeId");


            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryDepositPermission_TreasuryDepositPermissionTypesLookup_TypeId",
                table: "TreasuryDepositPermission",
                column: "TypeId",
                principalTable: "TreasuryDepositPermissionTypesLookup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreasuryWithdrawPermission_TreasuryWithdrawPermissionTypesLookup_TypeId",
                table: "TreasuryWithdrawPermission",
                column: "TypeId",
                principalTable: "TreasuryWithdrawPermissionTypesLookup",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryDepositPermission_TreasuryDepositPermissionTypesLookup_TypeId",
                table: "TreasuryDepositPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_TreasuryWithdrawPermission_TreasuryWithdrawPermissionTypesLookup_TypeId",
                table: "TreasuryWithdrawPermission");

            migrationBuilder.DropTable(
                name: "AssetsLookup");


            migrationBuilder.DropTable(
                name: "ExpensesLookup");

            migrationBuilder.DropTable(
                name: "TreasuryDepositPermissionTypesLookup");

            migrationBuilder.DropTable(
                name: "TreasuryWithdrawPermissionTypesLookup");

            migrationBuilder.DropIndex(
                name: "IX_TreasuryWithdrawPermission_TypeId",
                table: "TreasuryWithdrawPermission");

            migrationBuilder.DropIndex(
                name: "IX_TreasuryDepositPermission_TypeId",
                table: "TreasuryDepositPermission");

            migrationBuilder.DropColumn(
                name: "BenificiaryCode",
                table: "TreasuryWithdrawPermission");

            migrationBuilder.DropColumn(
                name: "Listed",
                table: "TreasuryWithdrawPermission");

            migrationBuilder.DropColumn(
                name: "BenificiaryCode",
                table: "TreasuryDepositPermission");

            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "TreasuryWithdrawPermission",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "TreasuryDepositPermission",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
