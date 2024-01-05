using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;
using Microsoft.EntityFrameworkCore;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class TransactionEntityRepository : Repository<TransactionEntity, TransactionEntityId>, ITransactionEntityRepository
    {
        public TransactionEntityRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<TransactionEntity>> BrowseUserTransactionEntitiesAsync()
        {
            return await DbContext.Set<TransactionEntity>()
                .AsNoTracking()
                .Where(te => true /*te.UserId == userId*/)
                .ToListAsync();
        }
    }
}
