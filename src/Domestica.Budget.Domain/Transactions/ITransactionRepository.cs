using DateKit.DB;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId);
        Task<IEnumerable<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId);
        Task<IEnumerable<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId);

        Task<IEnumerable<Transaction>> GetTransactionsByDateAndCategoryAsync(
            DateTimeRange dateTimePeriod,
            TransactionCategory category);
    }
}
