using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionSubcategories;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class TransactionSubcategoryRepository : Repository<TransactionSubcategory, TransactionSubcategoryId>, ITransactionSubcategoryRepository
    {
        public TransactionSubcategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TransactionSubcategory?> GetByValueAsync(
            TransactionSubcategoryValue value,
            TransactionCategory category,
            CancellationToken cancellationToken)
        {
            return await DbContext.Set<TransactionSubcategory>()
                .FirstOrDefaultAsync(sc => sc.Value == value && sc.CategoryId == category.Id);
        }
    }
}
