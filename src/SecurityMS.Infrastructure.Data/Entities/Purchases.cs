using MNS.Repository;
using System;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class Purchases : BaseEntity<long>
    {
        public DateTime PurchaseDate { get; set; }
        public long SupplierId { get; set; }
        public int SupplyTypeId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual SupplyTypes SupplyType { get; set; }
    }
}
