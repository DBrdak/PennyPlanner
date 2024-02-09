using System.Linq.Expressions;

namespace Domestica.Budget.Domain.TransactionEntities
{
    public interface ITransactionEntityRepository
    {
        Task<TransactionEntity> GetByIdAsync(TransactionEntityId id, CancellationToken cancellationToken = default);

        Task<TransactionEntity?> GetByIdIncludeAsync<TProperty>(
            TransactionEntityId id,
            Expression<Func<TransactionEntity, TProperty>> includeExpression,
            CancellationToken cancellationToken = default);

        Task<TEntity?> GetByNameIncludeAsync<TEntity, TProperty>(
            TransactionEntityName name,
            Expression<Func<TransactionEntity, TProperty>> includeExpression,
            CancellationToken cancellationToken = default) where TEntity : TransactionEntity;

        Task<List<TransactionEntity>> BrowseUserTransactionEntitiesAsync(/*UserId userId*/);
        Task AddAsync(TransactionEntity entity, CancellationToken cancellationToken);
        //Task<IEnumerable<TransactionEntity>> BrowseForUserAsync(User user);
        void Remove(TransactionEntity transactionEntity);
    }
}
