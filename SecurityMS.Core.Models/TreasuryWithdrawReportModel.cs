using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Core.Models
{
    public class TreasuryWithdrawReportModel
    {
        public int id { get; set; }
        [Display(Name = "نوع الحركة")]
        public string name { get; set; }
        [Display(Name = "المجموع")]
        public double total { get; set; }
        [Display(Name ="عدد الاذونات")]
        public long count { get; set; }
    }
}
