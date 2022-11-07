using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class SupplyTypes : BaseEntity<int>
    {
        [Display(Name = "نوع التوريد")]
        [Required(ErrorMessage = "الرجاء ادخال نوع التوريد")]
        public string SupplyName { get; set; }
    }
}
