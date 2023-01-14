using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMS.Core.Models
{
    public class TransactionsModel
    {
        public BankAccountsEntity Bank { get; set; }

        public IEnumerable<BankTransactions> Transactions { get; set; }
    }
}
