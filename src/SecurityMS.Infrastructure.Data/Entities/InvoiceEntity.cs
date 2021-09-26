using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityMS.Infrastructure.Data.Entities
{
    [Table("Invoices")]
    public class InvoiceEntity
    {
        [Display(Name = "رقم الفاتورة")]
        [Key]
        public long Id { get; set; }
        public long CompanyId { get; set; }
        [Display(Name = "أسم العميل")]
        public string CompanyName { get; set; }
        [Display(Name = "تاريخ الفاتورة")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "صافى المطلوب")]
        public decimal FinalIncome { get; set; }
        public virtual List<InvoiceDetails> items { get; set; }
    }
}
