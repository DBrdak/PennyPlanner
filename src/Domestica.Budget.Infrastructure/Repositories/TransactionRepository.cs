using DateKit.DB;
using Domestica.Budget.Domain.Accounts;
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

        public async Task<IEnumerable<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId)
        {
            return await DbContext.Set<Transaction>()
                .AsNoTracking()
                .Where(t => t.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t.SenderId == senderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t.RecipientId == recipientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateAndCategoryAsync(
            DateTimeRange dateTimePeriod,
            TransactionCategory category)
        {
            return await DbContext.Set<Transaction>()
                .Where(
                    t => t.TransactionDateUtc.Date >= dateTimePeriod.Start &&
                         t.TransactionDateUtc <= dateTimePeriod.End &&
                         t.Category == category)
                .ToListAsync();
        }
    }
}
