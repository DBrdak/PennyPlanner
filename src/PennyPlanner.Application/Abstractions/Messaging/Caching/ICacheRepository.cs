﻿using Responses.DB;

namespace PennyPlanner.Application.Abstractions.Messaging.Caching
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
