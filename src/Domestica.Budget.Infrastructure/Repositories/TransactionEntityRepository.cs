using System.Linq.Expressions;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Domain.TransactionEntities;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class TransactionEntityRepository : Repository<TransactionEntity, TransactionEntityId>, ITransactionEntityRepository
    {
        public TransactionEntityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TEntity?> GetByNameIncludeAsync<TEntity, TProperty>(
            TransactionEntityName name,
            Expression<Func<TransactionEntity, TProperty>> includeExpression,
            CancellationToken cancellationToken = default) where TEntity : TransactionEntity
        {
            return await DbContext.Set<TransactionEntity>()
            .Include(te => te.Transactions)
            .FirstOrDefaultAsync(
                    te => te.Name == name && te is TEntity,
                    cancellationToken /*te.UserId == userId*/) as TEntity;
        }

        public async Task<List<TransactionEntity>> BrowseUserTransactionEntitiesAsync()
        {
            return await DbContext.Set<TransactionEntity>()
                .AsNoTracking()
                .Include(te => te.Transactions)
                .Where(te => true /*te.UserId == userId*/)
                .ToListAsync();
        }
    }
}
