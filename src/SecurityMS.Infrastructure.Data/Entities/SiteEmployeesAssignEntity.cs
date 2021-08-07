using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEmployeesAssign")]
    public class SiteEmployeesAssignEntity : BaseEntity<long>
    {
        [Required]
        [Display(Name ="الموقع ")]
        public long SiteEmployeeId { get; set; }
        [Required]
        [Display(Name ="الموظف ")]
        public long EmployeeId { get; set; }

        [Display(Name ="متاح")]
        public bool IsActive { get; set; } = true;
        [Display(Name ="الموقع ")]
        public virtual SiteEmployeesEntity SiteEmployee { get; set; }
        [Display(Name ="الموظف ")]
        public virtual EmployeesEntity Employee { get; set; }
    }
}
