using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery : ICachedQuery<List<TransactionEntityModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionEntities(null);
        public TimeSpan? Expiration => null;
    }
}
