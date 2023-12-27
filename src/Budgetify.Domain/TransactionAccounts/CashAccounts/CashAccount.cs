using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Money.DB;

namespace Budgetify.Domain.TransactionAccounts.CashAccounts
{
    public sealed class CashAccount : TransactionAccount
    {
        public CashAccount(TransactionAccountName name, Currency currency) : base(name, currency)
        {
        }
    }
}
