namespace Domestica.Budget.Domain.TransactionEntities
{
    public interface ITransactionEntityRepository
    {
        Task<TransactionEntity> GetByIdAsync(TransactionEntityId id, CancellationToken cancellationToken = default);

        Task<List<TransactionEntity>> BrowseUserTransactionEntitiesAsync(/*UserId userId*/);
        Task AddAsync(TransactionEntity entity, CancellationToken cancellationToken);
        //Task<IEnumerable<TransactionEntity>> BrowseForUserAsync(User user);
    }
}
