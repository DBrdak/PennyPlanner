using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;

namespace Domestica.Budget.Domain.TransactionEntities.TransactionRecipients
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
            if (!IsActive)
            {
                throw new DomainException<TransactionRecipient>("Cannot add transaction to inactive transaction recipient");
            }

            _transactions.Add(transaction);
        }
    }
}
