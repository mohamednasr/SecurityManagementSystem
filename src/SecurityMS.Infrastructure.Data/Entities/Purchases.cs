using MNS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class Purchases : BaseEntity<long>
    {
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "المورد")]
        public long SupplierId { get; set; }
        [Display(Name = "نوع الصنف")]
        public int SupplyTypeId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual SupplyTypes SupplyType { get; set; }
        public virtual List<PurchaseItem> Items { get; set; }
    }
}
