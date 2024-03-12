using Microsoft.EntityFrameworkCore;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionSubcategories;

namespace PennyPlanner.Infrastructure.Repositories
{
    public sealed class TransactionSubcategoryRepository : Repository<TransactionSubcategory, TransactionSubcategoryId>, ITransactionSubcategoryRepository
    {
        public TransactionSubcategoryRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }

        public async Task<TransactionSubcategory?> GetByValueAsync(
            TransactionSubcategoryValue value,
            TransactionCategory category,
            CancellationToken cancellationToken)
        {
            return await DbContext.Set<TransactionSubcategory>()
                .FirstOrDefaultAsync(sc => sc.Value == value && sc.CategoryId == category.Id && sc.UserId == UserId);
        }

        public async Task<TransactionSubcategory?> GetByIdAsync(TransactionSubcategoryId id, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            var query = DbContext.Set<TransactionSubcategory>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(sc => sc.Id == id && sc.UserId == UserId, cancellationToken);
        }
    }
}
