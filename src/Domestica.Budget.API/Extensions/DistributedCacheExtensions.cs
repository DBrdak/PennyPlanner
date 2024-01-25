using CommonAbstractions.DB.Messaging;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Buffers;
using System.Text.Json;
using Domestica.Budget.API.Extensions;
using CancellationToken = System.Threading.CancellationToken;
using System.Buffers;
using System.Text.Json;
using Responses.DB;
using System;
using System.Text.Json.Serialization;
using Domestica.Budget.Application.Accounts.AddAccount;
using Domestica.Budget.Application.DataTransferObjects;

namespace Domestica.Budget.API.Extensions;

internal static class DistributedCacheExtensions
{
    internal static DistributedCacheEntryOptions DefaultExpiration = new DistributedCacheEntryOptions
        { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60) };

    internal static async Task<Result<T>> GetCachedResponseAsync<T>(this IDistributedCache cache, string key, ISender sender, IQuery<T> query, CancellationToken cancellationToken = default)
    {
        var result = await cache.GetAsync(
            key,
            async token =>
            {
                var result = await sender.Send(query, token);

                return result;
            },
            DefaultExpiration,
            cancellationToken);

        return result;
    }

    private static ValueTask<T> GetAsync<T>(this IDistributedCache cache, string key, Func<CancellationToken, ValueTask<T>> getMethod,
        DistributedCacheEntryOptions? options = null, CancellationToken cancellation = default)
    {
        return GetAsyncShared<int, T>(cache, key, state: 0, getMethod, options, cancellation);
    }

    private static ValueTask<T> GetAsyncShared<TState, T>(IDistributedCache cache, string key, TState state, Delegate getMethod,
        DistributedCacheEntryOptions? options, CancellationToken cancellation)
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
                Func<TState, CancellationToken, ValueTask<T>> get => await get(state, cancellation),
                Func<TState, T> get => get(state),
                Func<CancellationToken, ValueTask<T>> get => await get(cancellation),
                Func<T> get => get(),
                _ => throw new ArgumentException(nameof(getMethod)),
            };
            //
            bytes = Serialize(result);
            if (options is null)
            {   
                await cache.SetAsync(key, bytes, cancellation);
            }
            else
            {
                await cache.SetAsync(key, bytes, options, cancellation);
            }//
            return result;
        }
    }

    private static T Deserialize<T>(byte[] bytes)
    {
        // TODO Sprawca to AccountDto - rozkiminić deserializację
        return JsonSerializer.Deserialize<T>(bytes)!;
    }

    private static byte[] Serialize<T>(T value)
    {
        var buffer = new ArrayBufferWriter<byte>();
        using var writer = new Utf8JsonWriter(buffer);
        JsonSerializer.Serialize(writer, value);
        return buffer.WrittenSpan.ToArray();
    }
}