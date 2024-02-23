using Domestica.Budget.Application.Messaging;
using Domestica.Budget.Application.Messaging.Caching;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery() : ICachedQuery<List<AccountModel>>
    {
        public CacheKey CacheKey => CacheKey.Accounts(null);
        public TimeSpan? Expiration => null;
    }
}
