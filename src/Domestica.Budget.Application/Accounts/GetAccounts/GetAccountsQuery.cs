using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Application.Messaging;
using Domestica.Budget.Application.Messaging.Caching;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery() : ICachedQuery<List<AccountDto>>
    {
        public CacheKey CacheKey => CacheKey.Accounts(null);
        public TimeSpan? Expiration => null;
    }
}
