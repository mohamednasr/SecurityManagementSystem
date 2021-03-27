using MNS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("ShiftTypesLookup")]
    public class ShiftTypesLookup : BaseEntity<long>
    {

        public string Name { get; set; }
    }
}
