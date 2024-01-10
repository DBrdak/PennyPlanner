using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Transactions;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.TransactionEntities
{
    public abstract class TransactionEntity : Entity<TransactionEntityId>
    {
        public TransactionEntityName Name { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
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
