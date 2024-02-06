using Responses.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
