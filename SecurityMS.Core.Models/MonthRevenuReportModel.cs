using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class MonthRevenuReportModel
    {
        public List<MonthRevenuReport> Data { get; set; }
        public MonthRevenuReportSearchModel searchModel { get; set; }
    }

    public class MonthRevenuReport
    {
        public long CompanyId { get; set; }
        [Display(Name ="أسم العميل")]
        public string CompanyName { get; set; }

        [Display(Name = "عدد المواقع")]
        public int SitesCount { get; set; }
        [Display(Name ="صافى المطلوب")]
        public decimal FinalIncome { get; set; }
    }

    public class MonthRevenuReportSearchModel
    {
        public string CompanyName { get; set; }
        public int Month { get; set; } = DateTime.Now.Month;

        public int Year { get; set; } = DateTime.Now.Year;
    }
}
