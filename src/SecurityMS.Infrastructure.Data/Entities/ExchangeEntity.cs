using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class ExchangeEntity : BaseEntity<long>
    {
        [Display(Name = "تاريخ اذن الصرف")]
        public DateTime ExchangeDate { get; set; }

        [Display(Name = "نوع الصرف")]
        public int ExchangeTypeId { get; set; }

        [Display(Name = "جهة الصرف")]
        public long? ExchangeTo { get; set; }

        [Display(Name = "أسم جهة الصرف")]
        public string ExchangeName { get; set; }
        [Display(Name = "الأصناف")]
        public virtual List<ExchangeItems> ExchangeItems { get; set; }

        [Display(Name = "نوع الصرف")]
        public virtual ExchangeTypesLookups ExchangeType { get; set; }
    }
}
