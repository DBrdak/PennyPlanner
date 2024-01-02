using Budgetify.Domain.Transactions.IncomingTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions.OugoingTransactions;
using Budgetify.Domain.TransactionEntities;

namespace Budgetify.Domain.TransactionEntities.TransactionRecipients
{
    public sealed class TransactionRecipient : TransactionEntity
    {
        private TransactionRecipient()
        { }

        public TransactionRecipient(TransactionEntityName name) : base(name)
        {
        }

        internal void AddTransaction(OutgoingTransaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}
