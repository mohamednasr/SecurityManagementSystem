using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class CompanyInfo : BaseEntity<int>
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
