using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;

namespace Domestica.Budget.Domain.TransactionEntities.TransactionSenders
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
            if (!IsActive)
            {
                throw new DomainException<TransactionSender>("Cannot add transaction to inactive transaction sender");
            }

            _transactions.Add(transaction);
        }
    }
}
