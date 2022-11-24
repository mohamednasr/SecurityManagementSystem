using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Government")]
    [Display(Name = "المحافظات")]
    public class GovernmentEntity : BaseEntity<long>
    {
        [Display(Name = "المحافظة")]
        public string Name { get; set; }
    }
}
