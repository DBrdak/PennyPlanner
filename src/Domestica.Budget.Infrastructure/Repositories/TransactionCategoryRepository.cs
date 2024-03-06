using System.Linq.Expressions;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.TransactionCategories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class TransactionCategoryRepository : Repository<TransactionCategory, TransactionCategoryId>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }


        public async Task<IEnumerable<TransactionCategory>> BrowseAllIncludeAsync<TProperty>(Expression<Func<TransactionCategory, TProperty>> includeExpression, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TransactionCategory>()
                .Include(includeExpression)
                .AsNoTracking()
                .Where(tc => tc.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TransactionCategory>> BrowseAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TransactionCategory>()
                .AsNoTracking()
                .Where(tc => tc.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<TransactionCategory?> GetByIdAsync(TransactionCategoryId id, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            var query = DbContext.Set<TransactionCategory>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == UserId, cancellationToken);
        }

        public async Task<TransactionCategory?> GetByIdIncludeAsync<TProperty>(
            TransactionCategoryId id,
            Expression<Func<TransactionCategory, TProperty>> includeExpression,
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TransactionCategory>()
                .Include(includeExpression)
                .FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == UserId, cancellationToken);
        }

        public async Task<TCategory?> GetByValueAsync<TCategory>(TransactionCategoryValue value, CancellationToken cancellationToken = default) where TCategory : TransactionCategory
        {
            return await DbContext.Set<TransactionCategory>()
                .FirstOrDefaultAsync(
                    tc => tc.Value == value && tc.UserId == UserId && tc is TCategory,
                    cancellationToken) as TCategory;
        }
    }
}
