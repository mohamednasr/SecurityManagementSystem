using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class SalaryReportEmployeesReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "الموظف")]
        public long EmployeeId { get; set; }

        public long SalaryReportId { get; set; }
        [Display(Name = "المرتب الأساسي")]
        public decimal BaseSalary { get; set; }

        [Display(Name = "الجزائات")]
        public decimal Penalities { get; set; }

        [Display(Name = "الحوافز")]
        public decimal Rewards { get; set; }

        [Display(Name = "دفعه سلفه سابقه")]
        public decimal AdvancePaymentInstallment { get; set; }

        [Display(Name = "الضرائب")]
        public decimal Fees { get; set; }
        [Display(Name = "خصومات اضافية")]
        public decimal ExtraDeductions { get; set; }
        [Display(Name = "الأجمالي")]
        public decimal TotalSalary { get; set; }

        [Display(Name = "الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

        public virtual SalaryReportDetails SalaryReport { get; set; }
        public decimal GetTotal()
        {
            return BaseSalary + Rewards - (Penalities + AdvancePaymentInstallment + Fees);
        }
    }
}
