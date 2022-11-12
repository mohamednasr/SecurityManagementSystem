using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Infrastructure.Data.Entities
{
    public class BankAccountsEntity : BaseEntity<int>
    {


        [Display(Name = "اسم البنك")]
        public string Name { get; set; }
        [Display(Name = "رقم ال IBAN")]
        public string IBAN { get; set; }
        [Display(Name = "العملة")]
        public string Currency { get; set; }
        [Display(Name = "الرصيد الابتدائي")]
        public double OpeningBalance { get; set; }
        [Display(Name = "الرصيد الحالي")]
        public double CurrentBalance { get; set; }

        public virtual List<BankTransactions> BankTransactions { get; set; }


    }
}
