using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Core.Models
{
    public class TreasuryWithdrawReportDateModel
    {
        [Required]
        [Display(Name = "من")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "الى")]

        public DateTime EndDate { get; set; }
    }
}
