using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class TransactionEntityRepository : Repository<TransactionEntity>, ITransactionEntityRepository
    {
        public TransactionEntityRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }
    }
}
