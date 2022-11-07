using MNS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Jobs")]
    public class JobsEntity : BaseEntity<long>
    {
        [Display(Name="أسم الوظيفة")]
        [Required(ErrorMessage ="يجب كتابة اسم الوظيفة")]
        public string Name { get; set; }
        [Display(Name="القسم")]
        [Range(1, Double.PositiveInfinity, ErrorMessage ="يجب اختيار القسم")]
        public long DepartmentId { get; set; }
        
        [Display(Name="القسم")]
        public virtual DepartmentsEntity Department { get; set; }
    }
}
