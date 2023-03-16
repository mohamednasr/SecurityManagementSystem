using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEquipments")]
    public class SiteEquipmentsEntity : BaseEntity<long>
    {
        [Required]
        [Display(Name = "الموقع")]
        public long SiteId { get; set; }
        [Required]
        public long EquipmentId { get; set; }

        [Display(Name = "قيمة المعده")]
        public decimal EquipmentValue { get; set; }
        [Display(Name = "عدد المعدات")]
        public int EquipmentCount { get; set; }
        [Display(Name = "الموقع")]
        public virtual SitesEntity Site { get; set; }
        [Display(Name = "المعدات")]
        public virtual EquipmentsEntity Equipment { get; set; }
    }
}
