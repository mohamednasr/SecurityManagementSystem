using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class bankTransactionsTypesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankAccountsEntityId",
                table: "BankTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankId",
                table: "BankTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_BankId",
                table: "BankTransactions");

            migrationBuilder.RenameColumn(
                name: "BankAccountsEntityId",
                table: "BankTransactions",
                newName: "BankAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_BankTransactions_BankAccountsEntityId",
                table: "BankTransactions",
                newName: "IX_BankTransactions_BankAccountId");

            migrationBuilder.CreateTable(
                name: "BankCashDepositTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    TransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCashDepositTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCashDepositTransaction_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankCashWithdrawTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    TransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCashWithdrawTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCashWithdrawTransaction_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankChequeDepositTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    TransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankChequeDepositTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankChequeDepositTransaction_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankChequeWithdrawTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    TransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankChequeWithdrawTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankChequeWithdrawTransaction_BankAccounts_BankId",
                        column: x => x.BankId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankCashDepositTransaction_BankId",
                table: "BankCashDepositTransaction",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCashWithdrawTransaction_BankId",
                table: "BankCashWithdrawTransaction",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankChequeDepositTransaction_BankId",
                table: "BankChequeDepositTransaction",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankChequeWithdrawTransaction_BankId",
                table: "BankChequeWithdrawTransaction",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankAccountId",
                table: "BankTransactions",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankAccountId",
                table: "BankTransactions");

            migrationBuilder.DropTable(
                name: "BankCashDepositTransaction");

            migrationBuilder.DropTable(
                name: "BankCashWithdrawTransaction");

            migrationBuilder.DropTable(
                name: "BankChequeDepositTransaction");

            migrationBuilder.DropTable(
                name: "BankChequeWithdrawTransaction");

            migrationBuilder.RenameColumn(
                name: "BankAccountId",
                table: "BankTransactions",
                newName: "BankAccountsEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_BankTransactions_BankAccountId",
                table: "BankTransactions",
                newName: "IX_BankTransactions_BankAccountsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_BankId",
                table: "BankTransactions",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankAccountsEntityId",
                table: "BankTransactions",
                column: "BankAccountsEntityId",
                principalTable: "BankAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_BankAccounts_BankId",
                table: "BankTransactions",
                column: "BankId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
