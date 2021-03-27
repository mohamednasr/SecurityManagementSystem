using MNS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Equipments")]
    public class EquipmentsEntity : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ManufactureId { get; set; }
        public long EquipmentTypeId { get; set; }
        public long EquipmentTotalCount { get; set; }

        public virtual EquipmentTypesLookup EquipmentType { get; set; }
        public virtual CountriesLookup Manufacturing { get; set; }

    }
}
