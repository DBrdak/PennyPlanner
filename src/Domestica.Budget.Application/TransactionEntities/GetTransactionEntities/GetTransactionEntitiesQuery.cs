using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery(UserIdentityId UserId) : ICachedQuery<List<TransactionEntityModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionEntities(UserId.Value);
        public TimeSpan? Expiration => null;
    }
}
