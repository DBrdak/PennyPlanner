using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
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
        private readonly IUserContext _userContext;

        public CacheInvalidationBehavior(ICacheRepository cacheRepository, ILogger<CacheInvalidationBehavior<TRequest, TResponse>> logger, IUserContext userContext)
        {
            _cacheRepository = cacheRepository;
            _logger = logger;
            _userContext = userContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using (LogContext.PushProperty("Caching", "Invalidation"))
            {
                var response = await next();

                var userId = _userContext.TryGetIdentityId();

                var keys = CacheKey.All(userId).ToList();

                LogCacheInvalidationStart(request);

                try
                {
                    keys.ForEach(RemoveKey);
                }
                catch (Exception e)
                {
                    _logger.LogError("Problem while invalidatig cache", e);
                }

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
