using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;

namespace Domestica.Budget.Application.Abstractions.Messaging
{
    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

    public interface ICachedQuery
    {
        CacheKey CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
