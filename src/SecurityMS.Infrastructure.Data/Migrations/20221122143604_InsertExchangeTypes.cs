using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class InsertExchangeTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"
                    INSERT INTO [dbo].[ExchangeTypesLookup]
                                   ([CreatedAt]
                                   ,[Name]
                                   ,[IsDeleted])
                     VALUES
                            (GetDate(), N'صرف شخصي', 0),
                            (GetDate(), N'صرف موقع', 0),
                            (GetDate(), N'صرف صيانة', 0),
                            (GetDate(), N'صرف هالك', 0)

                ";
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"Delete from [dbo].[ExchangeTypesLookup]";
            migrationBuilder.Sql(sqlScript);
        }
    }
}
