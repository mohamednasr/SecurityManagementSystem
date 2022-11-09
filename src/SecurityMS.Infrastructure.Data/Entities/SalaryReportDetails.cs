using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class SalaryReportDetails : BaseEntity<long>
    {
        [Display(Name = "تاريخ الراتب")]
        public DateTime SalaryDate { get; set; }
        [Display(Name = "الموقع")]
        public long SiteId { get; set; }

        [Display(Name = "الموقع")]
        public virtual SitesEntity Site { get; set; }

        public virtual List<SalaryReportEmployeesReport> EmployeesSalaries { get; set; }
    }
}
