using Domestica.Budget.Domain.Transactions;

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
            _transactions.Add(transaction);
        }
    }
}
