using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class EmployeesSalaryReportDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "الموظف")]
        public long EmployeeId { get; set; }

        public long SalaryReportId { get; set; }
        [Display(Name = "المرتب الأساسي")]
        public decimal BaseSalary { get; set; }

        [Display(Name = "نسبه تحمل التأمينات")]
        public decimal Insurance { get; set; }

        [Display(Name = "الجزائات")]
        public decimal Penalities { get; set; }

        [Display(Name = "الحوافز")]
        public decimal Rewards { get; set; }

        [Display(Name = "دفعه سلفه سابقه")]
        public decimal AdvancePaymentInstallment { get; set; }

        [Display(Name = "الضرائب")]
        public decimal Taxes { get; set; }
        [Display(Name = "خصومات اضافية")]
        public decimal ExtraDeductions { get; set; }
        [Display(Name = "الأجمالي")]
        public decimal TotalSalary { get; set; }

        [Display(Name = "الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

        public virtual SalaryReportDetails SalaryReport { get; set; }
        public decimal GetTotalWithoutTaxes()
        {
            return BaseSalary + Rewards - (Penalities + AdvancePaymentInstallment + Insurance + ExtraDeductions);
        }

        public void GetTotal()
        {
            this.TotalSalary = (BaseSalary + Rewards - (Penalities + AdvancePaymentInstallment + Insurance + ExtraDeductions + Taxes));
        }

        public void GetInsurance()
        {
            this.Insurance = Employee.InsuranceAmount.GetValueOrDefault(0) * Employee.InsurancePercentage.GetValueOrDefault(0);
        }


        public void CalculateTaxes(List<IncomeTaxesMatrix> matrix)
        {
            decimal totalSalary = GetTotalWithoutTaxes() * 12;
            decimal taxes = 0;
            foreach (var range in matrix)
            {
                decimal slice = totalSalary - range.RangeTo.GetValueOrDefault(0);
                if (slice > 0 && range.RangeTo.HasValue)
                {
                    var sliceAmount = (range.RangeTo.GetValueOrDefault(0) - range.RangeFrom);
                    taxes += sliceAmount * range.TaxesPercentage;
                    totalSalary = totalSalary - sliceAmount;
                }
                else
                {
                    taxes += totalSalary * range.TaxesPercentage;
                    decimal TaxesExemption = taxes * range.TaxesExemption;
                    taxes = taxes - Math.Round(TaxesExemption);
                    break;
                }
            }

            this.Taxes = taxes / 12;
        }
    }
}