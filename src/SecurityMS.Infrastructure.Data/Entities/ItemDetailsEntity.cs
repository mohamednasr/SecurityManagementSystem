using SecurityMS.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class ItemDetailsEntity : BaseEntity<long>
    {
        [Display(Name = "الصنف")]
        public long ItemId { get; set; }
        [Display(Name = "الموقع")]
        public long? AssignedTo { get; set; }
        [Display(Name = "نوع الصرف")]
        public int? AssignedToType { get; set; }
        [Display(Name = "حاله الصنف")]
        public int StatusId { get; set; }
        [Display(Name = "تاريخ الاضافه")]
        public DateTime AddedDate { get; set; }
        [Display(Name = "تاريخ التكهين")]
        public DateTime OutDate { get; set; }
        [Display(Name = "الموقع")]
        public virtual ItemEntity Item { get; set; }
    }
}
