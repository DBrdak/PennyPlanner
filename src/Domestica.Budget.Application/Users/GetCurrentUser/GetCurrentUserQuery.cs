using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Users.GetCurrentUser
{
    /*
    public sealed record GetCurrentUserQuery() : ICachedQuery<UserModel>
    {
        public CacheKey CacheKey => CacheKey.Users(null);
        public TimeSpan? Expiration => null;
    }
    */
    public sealed record GetCurrentUserQuery() : IQuery<UserModel>
    {
        public CacheKey CacheKey => CacheKey.Users(null);
        public TimeSpan? Expiration => null;
    }
}
