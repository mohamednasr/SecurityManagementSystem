using System;
using System.ComponentModel.DataAnnotations;

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
