using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using MediatR;
using Microsoft.Extensions.Logging;
using Responses.DB;
using Serilog.Context;

namespace Domestica.Budget.Application.Behaviors
{
    public sealed class CacheInvalidationBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
        where TResponse : Result
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly ILogger<CacheInvalidationBehavior<TRequest,TResponse>> _logger;

        public CacheInvalidationBehavior(ICacheRepository cacheRepository, ILogger<CacheInvalidationBehavior<TRequest, TResponse>> logger)
        {
            _cacheRepository = cacheRepository;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("Caching", "Invalidation"))
            {
                var response = await next();

                //TODO fetch userId from IUserContext

                //TODO delete only one collection

                var keys = CacheKey.All(null).ToList();

                LogCacheInvalidationStart(request);
                
                keys.ForEach(RemoveKey);

                LogCacheInvalidationSuccess(request);

                return response;
            }
        }

        private void RemoveKey(CacheKey key)
        {
            LogCacheKeyInvalidationStart(key);

            try
            {
                _cacheRepository.RemoveKey(key);
            }
            catch (Exception e)
            {
                LogCacheKeyInvalidationFailure(key);
                throw;
            }

            LogCacheKeyInvalidationSuccess(key);
        }

        private void LogCacheInvalidationStart(TRequest request) => 
            _logger.LogInformation("Cache invalidation started for request: {requestType}", request.GetType().FullName);

        private void LogCacheInvalidationSuccess(TRequest request) =>
            _logger.LogInformation("Cache invalidation succeded for request: {requestType}", request.GetType().FullName);

        private void LogCacheKeyInvalidationStart(CacheKey key) =>
            _logger.LogInformation("Invalidating cache collection for key: {key}", key.ToString());

        private void LogCacheKeyInvalidationFailure(CacheKey key) =>
            _logger.LogError("Invalidation cache failed for key: {key}", key.ToString());

        private void LogCacheKeyInvalidationSuccess(CacheKey key) =>
            _logger.LogInformation("Invalidation cache succeed for key: {key}", key.ToString());
    }
}
