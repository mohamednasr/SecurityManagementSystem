using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class TreasuryDepositPermissionTypesLookup : BaseEntity<int>
    {
        [Display(Name = "نوع الحركة")]
        public string Name { get; set; }

    }
}
