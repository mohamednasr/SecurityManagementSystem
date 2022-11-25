using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class TreasuryWithdrawPermissionTypesLookup : BaseEntity<int>
    {
        [Display(Name = "نوع الحركة")]
        public string Name { get; set; }

    }

}
