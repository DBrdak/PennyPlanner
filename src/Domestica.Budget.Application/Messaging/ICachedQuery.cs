using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Caching;

namespace Domestica.Budget.Application.Messaging
{
    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

    public interface ICachedQuery
    {
        CacheKey CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
