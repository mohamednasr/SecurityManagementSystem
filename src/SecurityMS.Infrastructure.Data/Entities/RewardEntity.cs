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
        [Display(Name = "أسم الموظف")]
        public long EmployeeId { get; set; }
        [Display(Name = "نوع الحافز")]
        public int RewardType { get; set; }
        [Display(Name = "القيمه / الأيام")]
        public decimal Amount { get; set; }
        [Display(Name = "السبب")]
        public string Reason { get; set; }

        [Display(Name = "القيمه الاجماليه")]
        public decimal? RewardValue { get; set; }

        [Display(Name = "الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

        public string TypeString
        {
            get
            {
                switch (this.RewardType)
                {
                    case 0:
                        return "أيام";
                    case 1:
                        return "مبلغ";
                    default:
                        return "";
                }
            }
        }
    }
}
