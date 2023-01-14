using SecurityMS.Repository;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class UniformDetailsEntity : BaseEntity<long>
    {
        [Display(Name = "الزي")]
        public long UniformId { get; set; }
        [Display(Name = "الموظف")]
        public long? AssignedTo { get; set; }
        [Display(Name = "حاله الصنف")]
        public int StatusId { get; set; }
        [Display(Name = "تاريخ الاضافه")]
        public DateTime AddedDate { get; set; }
        [Display(Name = "تاريخ التكهين")]
        public DateTime OutDate { get; set; }
        [Display(Name = "الموظف")]
        public virtual EmployeesEntity Employee { get; set; }
        public virtual UniformEntity Uniform { get; set; }
    }
}
