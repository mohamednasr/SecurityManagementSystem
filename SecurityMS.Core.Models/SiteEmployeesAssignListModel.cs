using SecurityMS.Infrastructure.Data.Entities;

namespace SecurityMS.Core.Models
{
    public class SiteEmployeesAssignListModel : BaseModel<long>
    {
        public long EmployeeId { get; set; }
        public decimal EmployeeShiftSalary { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual EmployeesEntity Employee { get; set; }
    }
}
