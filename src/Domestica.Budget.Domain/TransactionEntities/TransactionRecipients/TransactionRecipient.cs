using Domestica.Budget.Domain.Transactions;

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
            _transactions.Add(transaction);
        }
    }
}
