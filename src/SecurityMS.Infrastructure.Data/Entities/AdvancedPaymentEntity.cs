﻿using SecurityMS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("AdvancedPayments")]
    public class AdvancedPaymentEntity : BaseEntity<long>
    {
        public long EmployeeId { get; set; }
        [Display(Name = "قيمة السلفه")]
        public double Amount { get; set; }
        [Display(Name = "تاريخ السلفه")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "تاريخ بداية السداد")]
        public DateTime InstallmentDate { get; set; }

        [Display(Name = "عدد اشهر السداد")]
        public int installments { get; set; } = 1;

        [Display(Name = "حاله الموافقه")]
        public bool IsAcceptable { get; set; }
        [Display(Name = "تاريخ الصرف")]
        public bool IsPayed { get; set; } = false;
        public string AcceptedBy { get; set; }
        [Display(Name = "تاريخ الموافقه")]
        public DateTime? AcceptedAt { get; set; }
        [Display(Name = "الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

    }
}
