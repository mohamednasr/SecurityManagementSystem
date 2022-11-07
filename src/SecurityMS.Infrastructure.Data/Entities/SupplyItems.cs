using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class SupplyItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Display(Name = "رقم أمر التوريد")]
        public long SupplyId { get; set; }
        [Display(Name = "الصنف")]
        public long ItemId { get; set; }
        [Display(Name = "عدد الصنف")]
        public int ItemQuantity { get; set; }

        [Display(Name = "أمر التوريد")]
        public virtual Supply Supply { get; set; }

        [Display(Name = "الصنف")]
        public virtual ItemEntity Item { get; set; }
    }
}
