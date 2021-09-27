using MNS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("AdvancedPayments")]
    public class AdvancedPaymentEntity : BaseEntity<long>
    {
        public long EmployeeId { get; set; }
        [Display(Name ="القيمه / الأيام")]
        public double Amount { get; set; }
        [Display(Name = "عدد اشهر السداد")]
        public int installments { get; set; } = 1;
       
        [Display(Name ="حاله الموافقه")]
        public bool IsAcceptable{ get; set; }
        [Display(Name ="تاريخ الصرف")]
        public DateTime PayedAt { get; set; } = DateTime.Now;
        public string PayedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string AcceptedBy { get; set; }
        public DateTime AcceptedAt { get; set; } = DateTime.Now;
        [Display(Name ="الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

    }
}
