using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Zones")]
    public class ZonesEntity : BaseEntity<long>
    {
        [Display(Name ="المنطقة")]
        public string Name { get; set; }
        public long GovernmentId { get; set; }
        
        public virtual GovernmentEntity Government { get; set; }
    }
}
