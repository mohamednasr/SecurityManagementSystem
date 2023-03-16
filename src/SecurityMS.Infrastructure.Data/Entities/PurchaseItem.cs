using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class PurchaseItem
    {
        public long PurchaseId { get; set; }
        [Display(Name = "الصنف")]
        public long ItemId { get; set; }
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }

        [Display(Name = "الكمية المورده")]
        public int SuppliedQuantity { get; set; } = 0;
        [Display(Name = "الصنف")]
        [ForeignKey(nameof(PurchaseId))]
        public Purchases Purchase { get; set; }

        [Display(Name = "الصنف")]
        public ItemEntity Item { get; set; }
    }
}
