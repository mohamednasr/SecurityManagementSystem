using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class Supplier : BaseEntity<long>
    {
        [Display(Name = "رقم المورد")]
        [Required(ErrorMessage = "الرجاء ادخال رقم المورد")]
        public string SupplierNumber { get; set; }
        [Display(Name = "أسم المورد")]
        [Required(ErrorMessage = "الرجاء ادخال أسم المورد")]
        public string SupplierName { get; set; }
        [Display(Name = "نوع التوريد")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "الرجاء أختيار نوع التوريد")]
        public int Type { get; set; }

        [Display(Name = "سجل تجاري")]
        public string CommercialNumber { get; set; }
        [Display(Name = "رقم التسجيل الضريبي")]
        public string TaxId { get; set; }
        [Display(Name = "رقم الملف الضريبي")]
        public string TaxFileNumber { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "تليفون")]
        public string Phone { get; set; }

        [Display(Name = "نوع التوريد")]
        public virtual SupplyTypes supplyType { get; set; }

    }
}
