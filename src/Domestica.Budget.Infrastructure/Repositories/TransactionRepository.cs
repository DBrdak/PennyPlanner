using DateKit.DB;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class TransactionRepository : Repository<Transaction, TransactionId>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .AsNoTracking()
                .Where(t => t.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<List<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t.SenderId == senderId)
                .ToListAsync();
        }

        public async Task<List<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t.RecipientId == recipientId)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByDateAndCategoryAsync(
            DateTimeRange dateTimePeriod,
            TransactionCategory category,
            CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(
                    t => t.TransactionDateUtc.Date >= dateTimePeriod.Start &&
                         t.TransactionDateUtc <= dateTimePeriod.End &&
                         t.Category == category)
                .ToListAsync();
        }

        public async Task<List<Transaction>> BrowseUserTransactions(CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => true /*t -> t.UserId == userId*/)
                .Include(t => t.Category)
                .ToListAsync(cancellationToken);
        }
    }
}
