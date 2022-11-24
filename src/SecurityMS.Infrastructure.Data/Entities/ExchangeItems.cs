using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class ExchangeItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Display(Name = "اذن الصرف")]
        public long ExchangeId { get; set; }
        [Display(Name = "الصنف")]
        public long ItemId { get; set; }
        [Display(Name = "عدد الصنف")]
        public int ItemQuantity { get; set; }

        [Display(Name = "أذن الصرف")]
        public virtual ExchangeEntity Exchange { get; set; }

        [Display(Name = "الصنف")]
        public virtual ItemEntity Item { get; set; }
    }
}
