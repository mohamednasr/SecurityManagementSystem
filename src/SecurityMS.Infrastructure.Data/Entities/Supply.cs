using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class Supply : BaseEntity<long>
    {
        [Display(Name = "رقم امر الشراء")]
        public long PurchaseId { get; set; }

        [Display(Name = "كود فاتورة التوريد")]
        public string SupplyCode { get; set; }

        [Display(Name = "تاريخ التوريد")]
        public DateTime SupplyDate { get; set; }

        [Display(Name = "امر الشراء")]
        public virtual Purchases Purchase { get; set; }
        [Display(Name = "اصناف التوريد")]
        public virtual List<SupplyItems> SupplyItems { get; set; }
    }
}
