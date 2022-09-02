using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class AttendanceModel
    {
        [Display(Name = "أسم الموظف")]
        public long EmployeeId { get; set; }
        [Display(Name = "الحاله")]
        public long AttendanceStatusId { get; set; }

        [Display(Name = "الفتره")]
        public long ShiftId { get; set; }

        //public LookupModel Employee { get; set; }
    }
}
