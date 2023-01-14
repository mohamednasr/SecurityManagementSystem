using MNS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class CompanyInfo: BaseEntity<int>
    {
        [Display(Name = "الأسم")]
        public string Name { get; set; }

        [Display(Name = "الشعار")]
        public string LogoUri { get; set; }

        [Display(Name = "سجل تجاري")]
        public string CommercialNumber { get; set; }

        [Display(Name = "بطاقة ضريبية")]
        public string TaxId { get; set; }
        [Display(Name = "ملف ضريبي")]
        public string TaxFileNumber { get; set; }

        [Display(Name = "العنوان")]
        public string Address { get; set; }

    }
}
