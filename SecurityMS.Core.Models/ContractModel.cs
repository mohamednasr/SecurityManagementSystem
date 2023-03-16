using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Core.Models
{
    public class ContractModel : BaseModel<long>
    {
        [Display(Name = "تاريخ التعاقد")]
        public DateTime Date { get; set; }

        [Display(Name = "تاريخ بدء التنفيذ")]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ انتهاء التنفيذ")]
        public DateTime EndDate { get; set; }
        [Display(Name = "العميل")]
        public long CustomerId { get; set; }
        [Display(Name = "جهة التواصل")]
        public long? ContractContactPersonId { get; set; }
        [Display(Name = "العميل")]
        public CustomersEntity Customer { get; set; }
        [Display(Name = "المواقع")]
        public List<SiteModel> ContractSites { get; set; }
        [Display(Name = "جهة التواصل")]
        public CustomerContactsEntity ContactPerson { get; set; }
    }
}
