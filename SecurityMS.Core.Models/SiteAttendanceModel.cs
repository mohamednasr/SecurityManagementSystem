using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class SiteAttendanceModel
    {
        [Display(Name = "الموقع")]
        public long SiteId { get; set; }

        [Display(Name = "التاريخ")]
        public DateTime AttendanceDate { get; set; }

        public List<AttendanceModel> EmployeesStatus { get;set; } 
    }
}
