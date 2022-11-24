using SecurityMS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Rewards")]
    public class RewardEntity : BaseEntity<long>
    {
        [Display(Name = "تاريخ الحافز")]
        public DateTime RewardDate { get; set; } = DateTime.Now;
        public long EmployeeId { get; set; }
        [Display(Name = "نوع الحافز")]
        public int RewardType { get; set; }
        [Display(Name = "القيمه / الأيام")]
        public decimal Amount { get; set; }
        [Display(Name = "السبب")]
        public string Reason { get; set; }

        public decimal? RewardValue { get; set; }

        [Display(Name = "الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

    }
}
