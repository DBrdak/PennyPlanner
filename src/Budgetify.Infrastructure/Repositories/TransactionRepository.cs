using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.TransactionEntities;
using Budgetify.Domain.Transactions;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
using Microsoft.EntityFrameworkCore;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class TransactionRepository : Repository<Transaction, TransactionId>, ITransactionRepository
    {
        public TransactionRepository(BudgetifyContext dbContext) : base(dbContext)
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
                .Where(t => t is IncomingTransaction)
                .Cast<IncomingTransaction>()
                .Where(t => t.SenderId == senderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId)
        {
            return await DbContext.Set<Transaction>()
                .Where(t => t is OutgoingTransaction)
                .Cast<OutgoingTransaction>()
                .Where(t => t.RecipientId == recipientId)
                .ToListAsync();
        }
    }
}
