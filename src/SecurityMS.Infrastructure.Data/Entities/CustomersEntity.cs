using SecurityMS.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Customers")]
    [Display(Name = "العملاء")]
    public class CustomersEntity : BaseEntity<long>
    {
        [Display(Name = "أسم العميل")]
        public string Name { get; set; }

        [Display(Name = "نوع العميل")]
        public long CustomerTypeId { get; set; }

        [Display(Name = "العميل الرئيسي")]
        public long? ParentCustomerId { get; set; }

        [Display(Name = "سجل تجاري")]
        public string CommercialNumber { get; set; }

        [Display(Name = "رقم التسجيل الضريبي")]
        public string TaxId { get; set; }
        [Display(Name = "رقم الملف الضريبي")]
        public string TaxFileNumber { get; set; }
        [Display(Name = "العنوان")]
        public string Address { get; set; }
        [Display(Name = "المحافظة")]
        public long? GovernmentId { get; set; }
        public virtual CustomersEntity ParentCustomers { get; set; }
        public virtual CustomerTypesLookup CustomerType { get; set; }
        public virtual GovernmentEntity Government { get; set; }
        //[Display(Name = "جهات الاتصال")]
        //public virtual List<CustomerContactsEntity> ContactPersons { get; set; }
    }
}
