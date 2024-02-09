using Domestica.Budget.Application.Caching;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Application.Messaging;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery : ICachedQuery<List<TransactionEntityDto>>
    {
        public CacheKey CacheKey => CacheKey.TransactionEntities(null);
        public TimeSpan? Expiration => null;
    }
}
