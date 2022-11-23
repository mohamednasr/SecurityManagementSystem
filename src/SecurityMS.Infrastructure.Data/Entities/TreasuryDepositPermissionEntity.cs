using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class TreasuryDepositPermissionEntity : BaseEntity<long>
    {

        [Display(Name = "رقم نوع الحركة")]
        public int TypeId { get; set; }

        [Display(Name = "تاريخ الاذن")]
        public DateTime Date { get; set; }

        [Display(Name = "القيمة")]
        public long Value { get; set; }

        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Display(Name = "نوع الحركة")]
        public virtual TreasuryDepositPermissionTypesLookup Type { get; set; }

        [Display(Name = "الجهة المستفادة")]
        public long? BenificiaryCode { get; set; }


    }
}
