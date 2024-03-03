using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using MediatR;
using Microsoft.Extensions.Logging;
using Responses.DB;
using Serilog.Context;

namespace Domestica.Budget.Application.Behaviors
{
    public sealed class QueryCachingBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
        where TResponse : Result
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;

        public QueryCachingBehavior(ICacheRepository cacheRepository, ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
        {
            _cacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("Caching", "QueryCaching"))
            {
                LogQueryCachingStart(request);

                var cachedQuery = await _cacheRepository.GetCachedResponseAsync(
                    request.CacheKey.ToString(),
                    _ => next(),
                    request.Expiration,
                    cancellationToken);

                if (cachedQuery.IsFailure)
                {
                    LogQueryCachingFailure(request, cachedQuery);
                }
                else
                {
                    LogQueryCachingSuccess(request);
                }

                return cachedQuery;
            }
        }

        private void LogQueryCachingStart(TRequest request)
        {
            _logger.LogInformation(
                "Query caching started for query: {query} with key: {key}",
                request.GetType().FullName,
                request.CacheKey.ToString());
        }

        private void LogQueryCachingSuccess(TRequest request)
        {
            _logger.LogInformation(
                "Query caching succeded for query: {query} with key: {key}",
                request.GetType().FullName,
                request.CacheKey.ToString());
        }

        private void LogQueryCachingFailure(TRequest request, TResponse cachedQuery)
        {
            _logger.LogWarning(
                "Query caching failed for query: {query} with key: {key} error: {error}",
                request.GetType().FullName,
                request.CacheKey.ToString(),
                cachedQuery.Error);
        }
    }
}
