using PennyPlanner.Domain.TransactionCategories;

namespace PennyPlanner.Domain.TransactionSubcategories
{
    public interface ITransactionSubcategoryRepository
    {
        Task<TransactionSubcategory?> GetByValueAsync(TransactionSubcategoryValue value, TransactionCategory category, CancellationToken cancellationToken);
        Task<TransactionSubcategory?> GetByIdAsync(TransactionSubcategoryId id, CancellationToken cancellationToken, bool asNoTracking = false);
        Task AddAsync(TransactionSubcategory  transactionSubcategory, CancellationToken cancellationToken);
        void Remove(TransactionSubcategory transactionSubcategory);
    }
}
