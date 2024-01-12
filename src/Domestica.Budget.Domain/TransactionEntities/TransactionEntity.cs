using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.TransactionEntities
{
    public abstract class TransactionEntity : Entity<TransactionEntityId>
    {
        public TransactionEntityName Name { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        protected readonly List<Transaction> _transactions;
        public bool IsActive { get; private set; }

        protected TransactionEntity()
        { }

        protected TransactionEntity(TransactionEntityName name) : base(new TransactionEntityId())
        {
            Name = name;
            _transactions = new();
            IsActive = true;
        }

        public void ChangeName(TransactionEntityName newName)
        {
            if (!IsActive)
            {
                throw new DomainException<TransactionEntity>("Cannot change name of inactive transaction entity");
            }

            Name = newName;
        }

        public void Deactivate() => IsActive = false;
    }
}
