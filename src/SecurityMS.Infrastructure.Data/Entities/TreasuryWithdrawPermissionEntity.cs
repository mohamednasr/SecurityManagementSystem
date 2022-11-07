using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class TreasuryWithdrawPermissionEntity :BaseEntity<long>
    {
        [Display(Name = "تاريخ الاذن")]
        public DateTime Date { get; set; }

        [Display(Name = "القيمة")]
        public long Value { get; set; }

        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Display(Name = "نوع الحركة")]
        public string TypeId { get; set; }


    }
}
