using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data.Entities;

namespace SecurityMS.Infrastructure.Data
{
    public interface IAppDbContext
    {
        DbSet<BlackListEntity> BlackListEntity { get; set; }
    }
}
