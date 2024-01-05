using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.Transactions;

namespace Budgetify.Domain.TransactionEntities.TransactionRecipients
{
    public sealed class TransactionRecipient : TransactionEntity
    {
        private TransactionRecipient()
        { }

        public TransactionRecipient(TransactionEntityName name) : base(name)
        {
        }

        internal void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}
