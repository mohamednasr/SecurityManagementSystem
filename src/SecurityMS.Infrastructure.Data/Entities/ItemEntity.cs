using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class ItemEntity : BaseEntity<long>
    {
        [Display(Name = "كود")]
        public string Code { get; set; }
        [Display(Name = "الصنف")]
        public string Name { get; set; }
        [Display(Name = "نوع الصنف")]
        public int? TypeId { get; set; }
        [Display(Name = "إجمالى الكميه")]
        public int TotalCount { get; set; }
        [Display(Name = "إجمالى الكميه المتاحه")]
        public int AvailableTotalCount { get; set; }
        [Display(Name = "حد إعادة الطلب")]
        public int MinimumAlert { get; set; }

        [Display(Name = "نوع الصنف")]
        [ForeignKey("TypeId")]
        public virtual SupplyTypes SupplyType { get; set; }
        //public virtual List<ItemDetailsEntity> Items { get; set; }
    }
}
