using System.Linq.Expressions;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class TransactionEntityRepository : Repository<TransactionEntity, TransactionEntityId>, ITransactionEntityRepository
    {
        public TransactionEntityRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }

        public async Task<TransactionEntity?> GetByIdAsync(TransactionEntityId id, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            var query = DbContext.Set<TransactionEntity>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == UserId, cancellationToken);
        }

        public async Task<TransactionEntity?> GetByIdIncludeAsync<TProperty>(
            TransactionEntityId id,
            Expression<Func<TransactionEntity, TProperty>> includeExpression,
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TransactionEntity>()
                .Include(includeExpression)
                .FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == UserId, cancellationToken);
        }

        public async Task<TEntity?> GetByNameIncludeAsync<TEntity, TProperty>(
            TransactionEntityName name,
            Expression<Func<TransactionEntity, TProperty>> includeExpression,
            CancellationToken cancellationToken = default) where TEntity : TransactionEntity
        {
            return await DbContext.Set<TransactionEntity>()
            .Include(te => te.Transactions)
            .FirstOrDefaultAsync(
                    te => te.Name == name && te.UserId == UserId && te is TEntity,
                    cancellationToken) as TEntity;
        }

        public async Task<List<TransactionEntity>> BrowseUserTransactionEntitiesAsync()
        {
            return await DbContext.Set<TransactionEntity>()
                .AsNoTracking()
                .Include(te => te.Transactions)
                .Where(te => te.UserId == UserId)
                .ToListAsync();
        }
    }
}
