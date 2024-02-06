using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Caching;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Application.Messaging;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery() : ICachedQuery<List<AccountDto>>
    {
        public CacheKey CacheKey => CacheKey.Accounts(null);
        public TimeSpan? Expiration => null;
    }
}
