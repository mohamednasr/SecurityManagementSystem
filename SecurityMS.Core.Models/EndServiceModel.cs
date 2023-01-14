using DataAnnotationsExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class EndServiceModel
    {
        public long EmployeeId { get; set; }

        [Display(Name = "أسم الموظف")]
        public string EmployeeName { get; set; }

        [Display(Name = "تاريخ إنتهاء الخدمه")]
        public DateTime EndDate { get; set; }

        [Required]
        [Min(1)]
        [Display(Name = "السبب")]
        public int Reason { get; set; }

        [Display(Name = "ملاحظات")]
        public string Notes { get; set; }

        [Display(Name = "خصومات")]
        public decimal PenaltyAmount { get; set; }

        [Display(Name = "سبب الخصم")]
        public string PenaltyReason { get; set; }
    }
}
