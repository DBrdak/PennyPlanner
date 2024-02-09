using Responses.DB;

namespace Domestica.Budget.Application.Caching
{
    public interface ICacheRepository
    {
        Task<T> GetCachedResponseAsync<T>(
            string key,
            Func<CancellationToken, Task<T>> factory,
            TimeSpan? expiration,
            CancellationToken cancellationToken = default)
            where T : Result;

        void RemoveKey(CacheKey key);
    }
}
