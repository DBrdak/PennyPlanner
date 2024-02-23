using Domestica.Budget.Domain.TransactionCategories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class TransactionCategoryRepository : Repository<TransactionCategory, TransactionCategoryId>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<TCategory?> GetByValueAsync<TCategory>(TransactionCategoryValue value, CancellationToken cancellationToken = default) where TCategory : TransactionCategory
        {
            return await DbContext.Set<TransactionCategory>()
                .FirstOrDefaultAsync(tc => tc.Value == value && tc is TCategory, cancellationToken) as TCategory;
        }
    }
}
