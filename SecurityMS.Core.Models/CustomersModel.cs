using System.ComponentModel.DataAnnotations;
namespace SecurityMS.Core.Models
{
    public class CustomerModel : BaseModel<long>
    {
        [Required]
        [Display(Name = "أسم العميل")]
        public string Name { get; set; }

        [Display(Name = "نوع العميل")]
        public long CustomerTypeId { get; set; }

        [Display(Name = "الشركه التابع لها")]
        public long? ParentCustomerId { get; set; }
        [Required]

        [Display(Name = "السجل التجاري")]
        public string CommercialNumber { get; set; }
        [Required]
        [Display(Name = "رقم التسجيل الضريبي")]
        public string TaxId { get; set; }
        [Display(Name = "رقم الملف الضريبي")]
        public string TaxFileNumber { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "المنطقة")]
        public long? ZoneId { get; set; }
        [Display(Name = "المحافظه")]
        public long? GovernmentId { get; set; }

    }
}
