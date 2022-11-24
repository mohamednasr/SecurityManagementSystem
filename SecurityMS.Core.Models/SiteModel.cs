using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class SiteModel : BaseModel<long>
    {
        [Display(Name = "أسم الموقع")]
        public string Name { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "المنطقة")]
        public long ZoneId { get; set; }
        [Display(Name = "العقد")]
        public long ContractId { get; set; }

        [Display(Name = "الانتقالات الشهريه")]
        public double? Transportations { get; set; }
        [Display(Name = "المنطقة")]
        public ZonesEntity? zone { get; set; }

        public List<SiteEmployeesEntity> SiteEmployees { get; set; }
        public List<SiteEquipmentsEntity> SiteEquipments { get; set; }
    }
}
