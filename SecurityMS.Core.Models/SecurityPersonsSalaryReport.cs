using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class SecurityPersonsSalaryReportModel
    {
        public List<SecurityPersonsSalaryReport> Data { get; set; }
        public SalarySearchModel searchModel { get; set; }
    }

    public class SecurityPersonsSalaryReport
    {
        [Display(Name ="الأسم")]
        public string EmployeeName { get; set; }

        [Display(Name ="حضور")]
        public int Attendance { get; set; }

        [Display(Name ="غياب")]
        public int Absence { get; set; }

        [Display(Name ="اذن")]
        public int Apologizes { get; set; }

        [Display(Name ="راحه")]
        public int BreakDays { get; set; }

        [Display(Name ="أجازه")]
        public int Vacation { get; set; }

        [Display(Name ="صافى الراتب")]
        public decimal FinalSalary { get; set; }
    }

    public class SalarySearchModel
    {
        public string EmployeeName { get; set; }
        public int Month { get; set; } = DateTime.Now.Month;

        public int Year { get; set; } = DateTime.Now.Year;
    }
}
