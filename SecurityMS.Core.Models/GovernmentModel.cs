using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class GovernmentModel : BaseModel<long>
    {
        [Display(Name = "أسم المحافظه")]
        public string Name { get; set; }
    }
}
