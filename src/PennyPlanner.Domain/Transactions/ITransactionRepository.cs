using DateKit.DB;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;

namespace PennyPlanner.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId, CancellationToken cancellationToken);
        Task<List<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId, CancellationToken cancellationToken);
        Task<List<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId, CancellationToken cancellationToken);
        Task<Transaction?> GetByIdAsync(TransactionId transactionId, CancellationToken cancellationToken, bool asNoTracking = false);
        Task<List<Transaction>> GetTransactionsByDateAndCategoryAsync(
            DateTimeRange dateTimePeriod,
            TransactionCategory category,
            CancellationToken cancellationToken);
        void Remove(Transaction transaction);
        Task<List<Transaction>> BrowseUserTransactions(CancellationToken cancellationToken);
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken);
    }
}
