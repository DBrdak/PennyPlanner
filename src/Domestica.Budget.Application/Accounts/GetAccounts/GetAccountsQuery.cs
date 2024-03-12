using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery(UserId UserId) : ICachedQuery<List<AccountModel>>
    {
        public CacheKey CacheKey => CacheKey.Accounts(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
