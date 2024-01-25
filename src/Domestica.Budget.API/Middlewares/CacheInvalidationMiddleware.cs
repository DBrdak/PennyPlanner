using StackExchange.Redis;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;

namespace Domestica.Budget.API.Middlewares
{
    public sealed class CacheInvalidationMiddleware(RequestDelegate next, IDistributedCache cache)
    {
        public async Task Invoke(HttpContext context)
        {
            await next(context);

            //var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var collection = context.Request.Path.Value;

            if (collection is null)
            {
                return;
            }
            
            if (collection?.IndexOf('/') != -1)
            {
                collection = collection.Substring(0, collection.IndexOf('/'));
            };

            if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase) ||
                context.Request.Method.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
            {
                await cache.RemoveAsync($"{collection}");
            }

        }
        
    }
}
