using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
using CommonAbstractions.DB.Entities;
#pragma warning disable CS8618

namespace Budgetify.Domain.TransactionEntities
{
    public abstract class TransactionEntity : Entity<TransactionEntityId>
    {
        public TransactionEntityName Name { get; private set; }
        public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();
        protected readonly List<Transaction> _transactions;

        protected TransactionEntity()
        { }

        protected TransactionEntity(TransactionEntityName name) : base(new TransactionEntityId())
        {
            Name = name;
            _transactions = new();
        }

        public void ChangeName(TransactionEntityName newName)
        {
            Name = newName;
        }
    }
}
