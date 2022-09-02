using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("ShiftTypesLookup")]
    public class ShiftTypesLookup : BaseEntity<long>
    {

        [Display(Name ="الفتره")]
        public string Name { get; set; }
    }
}
