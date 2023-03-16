using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMS.Infrastructure.Data.Migrations
{
    public partial class insertSuppliesFromType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"
                    INSERT INTO [dbo].[SuppliersTypes]
                                   ([CreatedAt]
                                   ,[Name]
                                   ,[IsDeleted])
                     VALUES
                            (GetDate(), N'توريد مورد', 0),
                            (GetDate(), N'توريد موقع', 0),
                            (GetDate(), N'توريد شخصي', 0),
                            (GetDate(), N'توريد صيانة', 0)

                ";
            migrationBuilder.Sql(sqlScript);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sqlScript = @"Delete from [dbo].[SuppliersTypes]";
            migrationBuilder.Sql(sqlScript);
        }
    }
}
