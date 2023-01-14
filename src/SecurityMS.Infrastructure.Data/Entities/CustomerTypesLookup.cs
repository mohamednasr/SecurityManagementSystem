using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("CustomerTypeLookup")]
    public class CustomerTypesLookup : BaseEntity<long>
    {
        [Display(Name = "نوع العميل")]
        public string Name { get; set; }
    }
}
