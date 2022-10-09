using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class PurchasesModel
    {
        public long SupplierId { get; set; }
        public int SupplyTypeId { get; set; }
        public virtual List<PurchaseItemModel> Items { get; set; }
    }

    public class PurchaseItemModel
    {
        [Display(Name = "الصنف")]
        public long ItemId { get; set; }
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }
    }
}
