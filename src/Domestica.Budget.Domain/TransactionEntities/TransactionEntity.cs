using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Shared;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.Users;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.TransactionEntities
{
    public abstract class TransactionEntity : IdentifiedEntity<TransactionEntityId>
    {
        public TransactionEntityName Name { get; private set; }
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        protected readonly List<Transaction> _transactions;

        protected TransactionEntity()
        { }

        protected TransactionEntity(TransactionEntityName name, UserIdentityId userId) : base(userId)
        {
            Name = name;
            UserId = userId;
            _transactions = new();
        }

        public void ChangeName(TransactionEntityName newName) => Name = newName;


        internal void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }
    }
}
