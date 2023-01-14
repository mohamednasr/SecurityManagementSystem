using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class BankCashWithdrawTransaction : BaseEntity<long>
    {

        [Display(Name = "تاريخ المعاملة")]
        public DateTime Date { get; set; }

        [Display(Name = "القيمة")]
        public double Value { get; set; }

        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [Display(Name = "اتجاه الحركة")]
        public string Direction { get; set; }


        [Display(Name = "البنك")]
        public int BankId { get; set; }

        [Display(Name = "رقم المعاملة")]
        public string TransactionNumber { get; set; }

        public virtual BankAccountsEntity BankAccount { get; set; }
    }
}
