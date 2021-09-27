using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Rewards")]
    public class RewardEntity : BaseEntity<long>
    {
        public long EmployeeId { get; set; }
        [Display(Name ="نوع الحافز")]
        public int RewardType { get; set; }
        [Display(Name ="القيمه / الأيام")]
        public double Amount { get; set; }
        [Display(Name ="السبب")]
        public long Reason { get; set; }
       
        [Display(Name ="الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

    }
}
