using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("AttendanceStatusLookup")]
    public class AttendanceStatusLookup : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
