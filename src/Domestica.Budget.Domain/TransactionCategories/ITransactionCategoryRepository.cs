using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace Domestica.Budget.Domain.TransactionCategories
{
    public interface ITransactionCategoryRepository
    {
        Task<IEnumerable<TransactionCategory>> BrowseAllIncludeAsync<TProperty>(
            Expression<Func<TransactionCategory, TProperty>> includeExpression,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TransactionCategory>> BrowseAllAsync(CancellationToken cancellationToken = default);
        Task<TransactionCategory?> GetByIdAsync(TransactionCategoryId id, CancellationToken cancellationToken = default);
        Task<TransactionCategory?> GetByIdIncludeAsync<TProperty>(
            TransactionCategoryId id,
            Expression<Func<TransactionCategory, TProperty>> includeExpression,
            CancellationToken cancellationToken = default);

        Task<TCategory?> GetByValueAsync<TCategory>(
            TransactionCategoryValue id,
            CancellationToken cancellationToken = default) where TCategory : TransactionCategory;
        Task AddAsync(TransactionCategory transactionCategory, CancellationToken cancellationToken = default);
        void Remove(TransactionCategory entity);
    }
}
