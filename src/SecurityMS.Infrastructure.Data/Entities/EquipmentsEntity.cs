using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Equipments")]
    public class EquipmentsEntity : BaseEntity<long>
    {
        [Display(Name ="أسم المعدة")]
        public string Name { get; set; }
        [Display(Name ="تفاصيل المعدة")]
        public string Description { get; set; }
        [Display(Name ="بلد المنشأ")]
        public long ManufactureId { get; set; }
        [Display(Name ="نوع المعدة")]
        public long EquipmentTypeId { get; set; }
        [Display(Name ="عدد المعدات")]
        public long EquipmentTotalCount { get; set; }

        [Display(Name ="نوع المعدة")]
        public virtual EquipmentTypesLookup EquipmentType { get; set; }
        [Display(Name ="بلد المنشأ")]
        public virtual CountriesLookup Manufacturing { get; set; }

    }
}
