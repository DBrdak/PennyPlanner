using DateKit.DB;
using Microsoft.EntityFrameworkCore;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Infrastructure.Repositories
{
    public sealed class TransactionRepository : Repository<Transaction, TransactionId>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }

        public async Task<List<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .AsNoTracking()
                .Where(t => t.AccountId == accountId && t.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t.SenderId == senderId && t.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t.RecipientId == recipientId && t.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Transaction?> GetByIdAsync(TransactionId transactionId, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = DbContext.Set<Transaction>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(t => t.Id == transactionId && t.UserId == UserId, cancellationToken);
        }

        public async Task<List<Transaction>> GetTransactionsByDateAndCategoryAsync(
            DateTimeRange dateTimePeriod,
            TransactionCategory category,
            CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(
                    t => t.TransactionDateUtc >= dateTimePeriod.Start &&
                         t.TransactionDateUtc <= dateTimePeriod.End &&
                         t.Category == category && 
                         t.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Transaction>> BrowseUserTransactions(CancellationToken cancellationToken)
        {
            return await DbContext.Set<Transaction>()
                .Where(t =>  t.UserId == UserId)
                .OrderByDescending(t => t.TransactionDateUtc)
                .Include(t => t.Category)
                .Include(t => t.Subcategory)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
