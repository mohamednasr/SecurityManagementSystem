using MNS.Repository;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("EquipmentDetails")]
    public class EquipmentDetailsEntity : BaseEntity<long>
    {
        public long EquipmentId { get; set; }
        public string Serial { get; set; }
        public DateTime ProductionDate { get; set; }
        public decimal EquipmentPrice { get; set; }
        public virtual EquipmentsEntity Equipment { get; set; }
    }
}
