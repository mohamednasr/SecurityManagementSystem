using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEmployees")]
    public class SiteEmployeesEntity : BaseEntity<long>
    {
        [Required]
        public long SiteId { get; set; }
        [Required]
        public long JobId { get; set; }
        [Required]
        public long ShiftTypeId { get; set; }
        public decimal ShiftValue { get; set; }
        public int EmployeesPerShift { get; set; }
        public virtual SitesEntity Site { get; set; }
        public virtual JobsEntity Job { get; set; }
        public virtual ShiftTypesLookup ShiftType { get; set; }
    }
}
