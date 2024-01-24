using Microsoft.Extensions.Caching.Distributed;

namespace Domestica.Budget.API.Extensions
{
    internal sealed class CacheOptions
    {
        internal static DistributedCacheEntryOptions DefaultExpiration = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)};
    }
}
