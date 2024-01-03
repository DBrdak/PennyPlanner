using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Transaction>> BrowseAccountTransactionsAsync(string accountId)
        {
            return null;
        }

        public async Task<IEnumerable<Transaction>> BrowseEntityTransactionsAsync(string transactionEntityId)
        {
            return null;
        }
    }
}
