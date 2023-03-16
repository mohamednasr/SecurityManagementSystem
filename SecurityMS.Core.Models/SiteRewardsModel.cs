using DataAnnotationsExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class SiteRewardsModel
    {
        [Display(Name = "الموقع")]
        [Required]
        [Min(1)]
        public long SiteId { get; set; }

        [Display(Name = "تاريخ المكافأه")]
        public DateTime RewardDate { get; set; } = DateTime.Now;

        [Display(Name = "نوع المكافأه")]
        public int RewardType { get; set; }
        [Display(Name = "القيمه / الأيام")]
        [Required]
        [Min(1)] 
        public decimal Amount { get; set; }
        [Display(Name = "السبب")]
        public string Reason { get; set; }
    }
}
