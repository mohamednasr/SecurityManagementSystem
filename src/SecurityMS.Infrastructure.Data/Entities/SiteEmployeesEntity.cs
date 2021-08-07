using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("SiteEmployees")]
    public class SiteEmployeesEntity : BaseEntity<long>
    {
        [Required]
        [Display(Name ="الموقع ")]
        public long SiteId { get; set; }
        [Required]
        [Display(Name ="الوظيفة ")]
        public long JobId { get; set; }
        [Required]
        [Display(Name ="الفتره ")]
        public long ShiftTypeId { get; set; }
        [Display(Name ="قيمة الفتره ")]
        public decimal ShiftValue { get; set; }
        [Display(Name = "مرتب الفتره ")]
        public decimal EmployeeShiftSalary { get; set; }
        [Display(Name ="عدد الأفراد")]
        public int EmployeesPerShift { get; set; }
        [Display(Name ="الموقع ")]
        public virtual SitesEntity Site { get; set; }
        [Display(Name ="الوظيفة ")]
        public virtual JobsEntity Job { get; set; }
        [Display(Name ="الفتره ")]
        public virtual ShiftTypesLookup ShiftType { get; set; }
    }
}
