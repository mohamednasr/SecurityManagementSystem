using MNS.Repository;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class ItemEntity : BaseEntity<long>
    {
        [Display(Name = "كود")]
        public string Code { get; set; }
        [Display(Name = "الصنف")]
        public string Name { get; set; }
        [Display(Name = "إجمالى الكميه")]
        public int TotalCount { get; set; }
        [Display(Name = "إجمالى الكميه المتاحه")]
        public int AvailableTotalCount { get; set; }
        [Display(Name = "حد إعادة الطلب")]
        public int MinimumAlert { get; set; }
        //public virtual List<ItemDetailsEntity> Items { get; set; }
    }
}
