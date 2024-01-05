using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.Transactions;

namespace Budgetify.Domain.TransactionEntities.TransactionSenders
{
    public sealed class TransactionSender : TransactionEntity
    {
        private TransactionSender()
        { }

        public TransactionSender(TransactionEntityName name) : base(name)
        {
        }

        internal void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}
