using DateKit.DB;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId, CancellationToken cancellationToken);
        Task<List<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId, CancellationToken cancellationToken);
        Task<List<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId, CancellationToken cancellationToken);
        Task<Transaction?> GetByIdAsync(TransactionId transactionId, CancellationToken cancellationToken);
        Task<List<Transaction>> GetTransactionsByDateAndCategoryAsync(
            DateTimeRange dateTimePeriod,
            TransactionCategory category,
            CancellationToken cancellationToken);
        void Remove(Transaction transaction);
        Task<List<Transaction>> BrowseUserTransactions(CancellationToken cancellationToken);
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
    }
}
