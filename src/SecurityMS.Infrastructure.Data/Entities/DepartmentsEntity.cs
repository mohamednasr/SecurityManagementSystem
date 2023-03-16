using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Departments")]
    public class DepartmentsEntity : BaseEntity<long>
    {
        [Display(Name = "أسم القسم")]
        [Required]
        public string Name { get; set; }
    }
}
