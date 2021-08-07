using System;

namespace SecurityMS.Core.Models
{
    //public class RevenuModel
    //{
    //    public int 
    //}

    public class RevenuesSearchModel
    {
        public string EmployeeName { get; set; }
        public int Month { get; set; } = DateTime.Now.Month;

        public int Year { get; set; } = DateTime.Now.Year;
    }
}
