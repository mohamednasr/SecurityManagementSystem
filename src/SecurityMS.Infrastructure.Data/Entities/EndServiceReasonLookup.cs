using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("EndServiceReasonLookup")]
    public class EndServiceReasonLookup : BaseEntity<int>
    {
        [Display(Name = "سبب الإنهاء")]
        public string Name { get; set; }
    }
    
}
