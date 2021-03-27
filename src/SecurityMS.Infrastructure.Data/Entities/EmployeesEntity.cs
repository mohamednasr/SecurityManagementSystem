using MNS.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Employees")]
    public class EmployeesEntity : BaseEntity<long>
    {
        [Required]
        [Display(Name = "أسم الموظف")]
        public string Name { get; set; }
        [Display(Name = "كود الموظف")]
        public string EmployeeCode { get; set; }
        [MaxLength(14,ErrorMessage = "الرقم القومى لا يتجاوز ال14 رقم")]
        [Display(Name = "الرقم القومي")]
        public string NationalId { get; set; }
        [Required]
        [Display(Name = "رقم التليفون")]
        public string Phone { get; set; }
        [Display(Name = "رقم تليفون بديل")]
        public string Phone2 { get; set; }
        [Display(Name = "تاريخ بدء الخدمة")]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ انتهاء الخدمة")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "الرقم التأميني")]
        public string InsuranceNumber { get; set; }
        [Display(Name = "الوظيفة")]
        public long JobId { get; set; }

        [Display(Name = "الوظيفة")]
        public virtual JobsEntity Job { get; set; }
    }
}
