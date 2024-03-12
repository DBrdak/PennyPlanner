using PennyPlanner.Application.Abstractions.Messaging;
using PennyPlanner.Application.Abstractions.Messaging.Caching;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery(UserId UserId) : ICachedQuery<List<TransactionEntityModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionEntities(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
