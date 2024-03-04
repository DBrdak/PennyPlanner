using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;

namespace Domestica.Budget.Application.Transactions.GetTransactions
{
    //TODO just like this
    /*
    public sealed record GetTransactionsQuery(IUserContext _userContext) : ICachedQuery<List<TransactionModel>>
    {
        private readonly IUserContext _userContext = _userContext;

        public CacheKey CacheKey => CacheKey.Transactions(_userContext.IdentityId);
        public TimeSpan? Expiration => null;
    }
    */
    public sealed record GetTransactionsQuery() : ICachedQuery<List<TransactionModel>>
    {
        public CacheKey CacheKey => CacheKey.Transactions(null);
        public TimeSpan? Expiration => null;
    }
}
