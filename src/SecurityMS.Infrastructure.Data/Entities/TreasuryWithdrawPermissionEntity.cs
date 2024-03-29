﻿using SecurityMS.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class TreasuryWithdrawPermissionEntity : BaseEntity<long>
    {

        [Display(Name = "رقم نوع الحركة")]
        public int TypeId { get; set; }

        [Display(Name = "تاريخ الاذن")]
        public DateTime Date { get; set; }

        [Display(Name = "القيمة")]
        public long Value { get; set; }

        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Display(Name = "نوع الحركة")]
        public virtual TreasuryWithdrawPermissionTypesLookup Type { get; set; }

        [Display(Name = "مقيد")]
        public bool? Listed { get; set; }

        [Display(Name = "الجهة المستفادة")]
        public long? BenificiaryCode { get; set; }


    }
}
