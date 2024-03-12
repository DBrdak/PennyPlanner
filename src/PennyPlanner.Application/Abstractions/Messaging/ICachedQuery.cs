using CommonAbstractions.DB.Messaging;
using PennyPlanner.Application.Abstractions.Messaging.Caching;

namespace PennyPlanner.Application.Abstractions.Messaging
{
    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

    public interface ICachedQuery
    {
        CacheKey CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
