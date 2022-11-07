using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("EquipmentTypesLookup")]
    public class EquipmentTypesLookup : BaseEntity<long>
    {
        [Display(Name ="نوع المعده")]
        public string Name { get; set; }
    }
}
