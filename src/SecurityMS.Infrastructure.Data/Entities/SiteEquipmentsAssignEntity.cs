using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEquipmentsAssign")]
    public class SiteEquipmentsAssignEntity : BaseEntity<long>
    {
        [Required]
        public long SiteEquipmenteId { get; set; }
        [Required]
        public long EquipmentId { get; set; }
        public bool IsActive { get; set; }
        public virtual SiteEquipmentsEntity SiteEquipment { get; set; }
        public virtual EquipmentDetailsEntity EquipmentDetails { get; set; }
    }
}
