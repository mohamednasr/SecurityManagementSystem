using MNS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEmployeesAttendance")]

    public class SiteEmployeeAttendanceEntity : BaseEntity<long>
    {
        [Display(Name = "أسم الموظف")]
        public long EmployeeId { get; set; }

        [Display(Name = "الموقع")]
        public long SiteId { get; set; }

        [Display(Name = "التاريخ")]
        public DateTime AttendanceDate { get; set; }

        [Display(Name = "الفتره")]
        public long ShiftId { get; set; }

        [Display(Name = "الحاله")]
        public long AttendanceStatusId { get; set; }

        [Range(0, 4, ErrorMessage = "لا يمكن ادخال خصم اكثر من 4 أيام")]
        [Display(Name = "الخصم (أيام)")]
        public int? Penality { get; set; }

        [Display(Name = "أسم الموظف")]
        public virtual EmployeesEntity Employee { get; set; }
        [Display(Name = "الموقع")]
        public virtual SitesEntity Site { get; set; }
        [Display(Name = "الفتره")]
        public virtual ShiftTypesLookup ShiftType { get; set; }
        [Display(Name = "الحاله")]
        public virtual AttendanceStatusLookup AttendanceStatus { get; set; }

    }
}
