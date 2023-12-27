using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Budgetify.Domain.Transactions;
using CommonAbstractions.DB.Entities;
using Money.DB;

namespace Budgetify.Domain.TransactionAccounts.BankAccounts
{
    public sealed class BankAccount : TransactionAccount
    {
        public BankAccountNumber Number { get; private set; }

        public BankAccount(TransactionAccountName name, Currency currency, BankAccountNumber number) : base(name, currency)
        {
            Number = number;
        }
    }
}
