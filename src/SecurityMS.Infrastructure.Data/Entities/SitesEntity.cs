using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Sites")]
    public class SitesEntity : BaseEntity<long>
    {
        [Display(Name = "أسم الموقع")]
        public string Name { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "المنطقة")]
        public long ZoneId { get; set; }
        [Display(Name = "المنطقة")]
        public virtual ZonesEntity zone { get; set; }

        public virtual ContractsEntity Contracts { get; set; }
    }
}
