using Budgetify.Domain.TransactionAccounts.BankAccounts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CommonAbstractions.DB.Entities;
using Money.DB;

namespace Budgetify.Domain.TransactionAccounts
{
    public abstract class TransactionAccount : Entity
    {
        public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();
        private List<Transaction> _transactions;
        public TransactionAccountName Name { get; private set; }
        public Currency Currency { get; private set; }

        protected TransactionAccount(TransactionAccountName name, Currency currency) : base()
        {
            Currency = currency;
            Name = name;
        }
    }
}
