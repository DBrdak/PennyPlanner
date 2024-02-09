using Domestica.Budget.Application.Caching;
using Domestica.Budget.Application.Messaging;
using MediatR;
using Responses.DB;

namespace Domestica.Budget.Application.Behaviors
{
    public sealed class QueryCachingBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
        where TResponse : Result
    {
        private readonly ICacheRepository _cacheRepository;

        public QueryCachingBehavior(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await _cacheRepository.GetCachedResponseAsync(
                request.CacheKey.ToString(),
                _ => next(),
                request.Expiration,
                cancellationToken);
        }
    }
}
