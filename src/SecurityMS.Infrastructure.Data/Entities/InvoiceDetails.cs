using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("InvoicesDetails")]
    public class InvoiceDetails
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = "العدد")]
        public int Count { get; set; }
        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [Display(Name = "الاجمالي")]
        public decimal Total { get; set; }
        [Display(Name = "البند")]
        public string ItemName { get; set; }

        public long InvoiceId { get; set; }

        public virtual InvoiceEntity invoice { get; set; }
    }
}
