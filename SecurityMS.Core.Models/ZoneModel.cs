using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class ZoneModel : BaseModel<long>
    {
        [Display(Name = "أسم المنطقه")]
        public string Name { get; set; }
        [Display(Name = "المحافظه")]
        public long GovernmentId { get; set; }

        [Display(Name = "المحافظه")]
        public GovernmentModel Government { get; set; }
    }
}
