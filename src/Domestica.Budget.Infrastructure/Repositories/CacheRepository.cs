﻿using Microsoft.Extensions.Caching.Distributed;
using Responses.DB;
using System.Buffers;
using System.Text.Json;
using Domestica.Budget.Application.Caching;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _defaultExpiration = new DistributedCacheEntryOptions
        { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

        public CacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetCachedResponseAsync<T>(
            string key,
            Func<CancellationToken, Task<T>> factory,
            TimeSpan? expiration,
            CancellationToken cancellationToken = default)
            where T : Result
        {
            var result = await GetAsync(
                _cache,
                key,
                factory,
                ParseExpriration(expiration) ?? _defaultExpiration,
                cancellationToken);

            return result;
        }

        public async void RemoveKey(CacheKey key) => await _cache.RemoveAsync(key.ToString());

        private static DistributedCacheEntryOptions? ParseExpriration(TimeSpan? expiration) => new DistributedCacheEntryOptions
            { AbsoluteExpirationRelativeToNow = expiration };

        private static ValueTask<T> GetAsync<T>(IDistributedCache cache, string key, Func<CancellationToken, Task<T>> getMethod,
            DistributedCacheEntryOptions? options = null, CancellationToken cancellation = default) where T : Result
        {
            return GetAsyncShared<int, T>(cache, key, state: 0, getMethod, options, cancellation);
        }

        private static ValueTask<T> GetAsyncShared<TState, T>(IDistributedCache cache, string key, TState state, Delegate getMethod,
            DistributedCacheEntryOptions? options, CancellationToken cancellation) where T : Result
        {
            var pending = cache.GetAsync(key, cancellation);
            if (!pending.IsCompletedSuccessfully)
            {
                return Awaited(cache, key, pending, state, getMethod, options, cancellation);
            }

            var bytes = pending.GetAwaiter().GetResult();
            if (bytes is null)
            {
                return Awaited(cache, key, null, state, getMethod, options, cancellation);
            }

            return new(Deserialize<T>(bytes));

            static async ValueTask<T> Awaited(
                IDistributedCache cache,
                string key,
                Task<byte[]?>? pending,
                TState state,
                Delegate getMethod,
                DistributedCacheEntryOptions? options,
                CancellationToken cancellation)
            {
                byte[]? bytes;
                if (pending is not null)
                {
                    bytes = await pending;
                    if (bytes is not null)
                    {
                        return Deserialize<T>(bytes);
                    }
                }
                var result = getMethod switch
                {
                    Func<TState, CancellationToken, Task<T>> get => await get(state, cancellation),
                    Func<TState, T> get => get(state),
                    Func<CancellationToken, Task<T>> get => await get(cancellation),
                    Func<T> get => get(),
                    _ => throw new ArgumentException(nameof(getMethod)),
                };

                bytes = Serialize(result);

                if (bytes is null)
                {
                    return result;
                }

                if (options is null)
                {
                    await cache.SetAsync(key, bytes, cancellation);
                }
                else
                {
                    await cache.SetAsync(key, bytes, options, cancellation);
                }
                return result;
            }
        }

        private static T Deserialize<T>(byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes, new JsonSerializerOptions { IncludeFields = true })!;
        }

        private static byte[]? Serialize<T>(T value) where T : Result
        {
            if (value.IsFailure)
            {
                return null;
            }

            var buffer = new ArrayBufferWriter<byte>();
            using var writer = new Utf8JsonWriter(buffer);
            JsonSerializer.Serialize(writer, value);
            return buffer.WrittenSpan.ToArray();
        }
    }
}