using MNS.Repository;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Sites")]
    public class SitesEntity : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public long ZoneId { get; set; }
        public virtual ZonesEntity zone { get; set; }

        public virtual ContractsEntity Contracts { get; set; }
    }
}
