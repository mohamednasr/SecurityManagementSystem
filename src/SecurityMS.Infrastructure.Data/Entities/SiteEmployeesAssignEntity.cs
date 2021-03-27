using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEmployeesAssign")]
    public class SiteEmployeesAssignEntity : BaseEntity<long>
    {
        [Required]
        public long SiteEmployeeId { get; set; }
        [Required]
        public long EmployeeId { get; set; }
        public decimal EmployeeShiftSalary { get; set; }
        public bool IsActive { get; set; }
        public virtual SiteEmployeesEntity SiteEmployee { get; set; }
        public virtual EmployeesEntity Employee { get; set; }
    }
}
