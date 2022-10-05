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

        [Range(0, 4, ErrorMessage = "لا يمكن ادخال خصم اكثر من 4 أيام")]
        [Display(Name = "الخصم (أيام)")]
        public int Penality { get; set; }
        //public LookupModel Employee { get; set; }
    }
}
