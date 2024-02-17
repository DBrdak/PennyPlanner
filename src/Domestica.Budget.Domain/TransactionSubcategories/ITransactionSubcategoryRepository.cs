using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Domain.TransactionSubcategories
{
    public interface ITransactionSubcategoryRepository
    {
        Task<TransactionSubcategory?> GetByValueAsync(TransactionSubcategoryValue value, TransactionCategory category, CancellationToken cancellationToken);
        Task<TransactionSubcategory?> GetByIdAsync(TransactionSubcategoryId id, CancellationToken cancellationToken);
        Task AddAsync(TransactionSubcategory  transactionSubcategory, CancellationToken cancellationToken);
        void Remove(TransactionSubcategory transactionSubcategory);
    }
}
