using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Repository
{
    public class LookupBaseEntity<T> : BaseEntity<T>
    {
        [Display(Name = "الأسم")]
        public string Name { get; set; }

    }
}
