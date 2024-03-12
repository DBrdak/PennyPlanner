using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery(UserId UserId) : ICachedQuery<List<TransactionEntityModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionEntities(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
