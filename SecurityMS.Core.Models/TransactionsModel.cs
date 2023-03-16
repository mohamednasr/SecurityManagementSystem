using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace SecurityMS.Core.Models
{
    public class TransactionsModel
    {
        public BankAccountsEntity Bank { get; set; }

        public IEnumerable<BankTransactions> Transactions { get; set; }
    }
}
