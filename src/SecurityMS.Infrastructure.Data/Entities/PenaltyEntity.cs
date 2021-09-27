﻿using MNS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Penalties")]
    public class PenaltyEntity : BaseEntity<long>
    {
        public long EmployeeId { get; set; }
        [Display(Name ="نوع الجزاء")]
        public int PenaltyType { get; set; }
        [Display(Name ="القيمه / الأيام")]
        public double Amount { get; set; }
        [Display(Name ="السبب")]
        public long Reason { get; set; }
       
        [Display(Name ="الموظف")]
        public virtual EmployeesEntity Employee { get; set; }

    }
}