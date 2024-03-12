using PennyPlanner.Application.Abstractions.Messaging;
using PennyPlanner.Application.Abstractions.Messaging.Caching;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery(UserId UserId) : ICachedQuery<List<AccountModel>>
    {
        public CacheKey CacheKey => CacheKey.Accounts(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
