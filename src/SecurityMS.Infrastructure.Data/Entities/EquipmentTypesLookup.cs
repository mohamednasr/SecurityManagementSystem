using MNS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("EquipmentTypesLookup")]
    public class EquipmentTypesLookup : BaseEntity<long>
    {
        public string Name { get; set; }
    }
}
