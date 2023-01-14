using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Equipments")]
    public class EquipmentsEntity : BaseEntity<long>
    {
        [Display(Name = "أسم المعده")]
        public string Name { get; set; }
        [Display(Name = "كود المعده")]
        public string Code { get; set; }
        [Display(Name = "بلد المنشأ")]
        public long ManufactureId { get; set; }
        [Display(Name = "نوع المعده")]
        public long EquipmentTypeId { get; set; }
        [Display(Name = "إجمالى الكميه")]
        public long EquipmentTotalCount { get; set; }

        [Display(Name = "الكميه المتوفره")]
        public long AvailableTotalCount { get; set; }
        [Display(Name = "حد إعادة الطلب")]
        public int MinimumAlert { get; set; }
        [Display(Name = "نوع المعده")]
        public virtual EquipmentTypesLookup EquipmentType { get; set; }
        [Display(Name = "بلد المنشأ")]
        public virtual CountriesLookup Manufacturing { get; set; }
    }
}
