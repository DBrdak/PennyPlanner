using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CommonAbstractions.DB.Entities;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
#pragma warning disable CS8618

namespace Budgetify.Domain.Transactions
{
    public abstract class Transaction : Entity
    {
        public Money.DB.Money TransactionAmount { get; private set; }
        public DateTime TransactionDateUtc { get; private set; }

        protected Transaction()
        { }

        protected Transaction(Money.DB.Money transactionAmount) : base()
        {
            TransactionAmount = transactionAmount;
            TransactionDateUtc = DateTime.UtcNow;
        }
    }
}
