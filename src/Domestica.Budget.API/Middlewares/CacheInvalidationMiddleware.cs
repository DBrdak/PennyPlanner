using Domestica.Budget.API.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace Domestica.Budget.API.Middlewares
{
    public sealed class CacheInvalidationMiddleware(RequestDelegate next, IDistributedCache cache)
    {
        public async Task Invoke(HttpContext context)
        {
            await next(context);

            if (IsNotCommandMethod(context))
            {
                return;
            }

            //var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var keys = CacheKey.All(null);
            
            keys.ToList().ForEach(RemoveKey); //TODO Add userId
        }

        async void RemoveKey(CacheKey key) => await cache.RemoveAsync(key.ToString());

        private bool IsNotCommandMethod(HttpContext context) =>
            !(context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
              context.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase) ||
              context.Request.Method.Equals("DELETE", StringComparison.OrdinalIgnoreCase));
    }
}
