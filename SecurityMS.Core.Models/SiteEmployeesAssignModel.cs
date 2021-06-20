using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class SiteEmployeesAssignModel : BaseModel<long>
    {
        [Display(Name = "الموقع")]
        public long SiteEmployeeId { get; set; }
        [Display(Name = "الموقع")]
        public SiteEmployeesEntity SiteEmployee { get; set; }
        
        public List<SiteEmployeesAssignListModel> SiteAssignedEmployees { get; set; }
    }
}
