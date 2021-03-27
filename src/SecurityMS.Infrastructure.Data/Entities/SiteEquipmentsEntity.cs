using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEquipments")]
    public class SiteEquipmentsEntity : BaseEntity<long>
    {
        [Required]
        public long SiteId { get; set; }
        [Required]
        public long EquipmentId { get; set; }
       
        public decimal EquipmentValue { get; set; }
        public int EquipmentCount { get; set; }
        public virtual SitesEntity Site { get; set; }
        public virtual EquipmentsEntity Equipment { get; set; }
    }
}
