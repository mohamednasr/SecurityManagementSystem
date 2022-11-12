using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class insertTaxesSlices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"
                    INSERT INTO [dbo].[IncomeTaxesMatrix]
                           ([RangeFrom]
                           ,[RangeTo]
                           ,[TaxesPercentage]
                           ,[TaxesExemption])
                     VALUES
                            (0, 15000, 0, 1),
                            (8000, 30000, 0.1, 0.85 ),
                            (30000, 45000, 0.15, 0.45 ),
                            (45000,  200000, 0.2, 0.075),
                            (200000, null, 0.225, 0 )
                ";
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"Delete from [dbo].[IncomeTaxesMatrix]";
            migrationBuilder.Sql(sqlScript);
        }
    }
}
