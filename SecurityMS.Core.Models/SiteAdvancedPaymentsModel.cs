using DataAnnotationsExtensions;
using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class SiteAdvancedPaymentsModel
    {
        [Display(Name = "الموقع")]
        [Required]
        [Min(1)]
        public long SiteId { get; set; }

        [Display(Name = "قيمة السلفه")]
        [Required]
        [Min(1)]
        public double Amount { get; set; }

        [Display(Name = "تاريخ السلفه")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Display(Name = "تاريخ بداية السداد")]
        public DateTime InstallmentDate { get; set; } = DateTime.Now;

        [Display(Name = "عدد اشهر السداد")]
        public int installments { get; set; } = 1;
    }
}
