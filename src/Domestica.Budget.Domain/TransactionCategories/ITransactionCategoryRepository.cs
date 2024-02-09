namespace Domestica.Budget.Domain.TransactionCategories
{
    public interface ITransactionCategoryRepository
    {
        Task<IEnumerable<TransactionCategory>> BrowseAllAsync(CancellationToken cancellationToken = default);
        Task<TransactionCategory?> GetByIdAsync(TransactionCategoryId id, CancellationToken cancellationToken = default);

        Task<TCategory?> GetByValueAsync<TCategory>(
            TransactionCategoryValue id,
            CancellationToken cancellationToken = default) where TCategory : TransactionCategory;
        Task AddAsync(TransactionCategory transactionCategory, CancellationToken cancellationToken = default);
        void Remove(TransactionCategory entity);
    }
}
