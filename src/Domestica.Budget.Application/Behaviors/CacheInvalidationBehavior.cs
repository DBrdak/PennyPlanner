using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Caching;
using MediatR;

namespace Domestica.Budget.Application.Behaviors
{
    public sealed class CacheInvalidationBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheInvalidationBehavior(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            var userId = GetUserIdFromRequest(request);

            //TODO delete only one collection

            var keys = CacheKey.All(userId).ToList();

            keys.ForEach(_cacheRepository.RemoveKey);

            return response;
        }

        private static string? GetUserIdFromRequest(TRequest request)
        {
            return request.GetType().GetProperty("UserId")?.GetValue(request) as string;
        }
    }
}
