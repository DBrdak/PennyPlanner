using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;

namespace Domestica.Budget.Application.Transactions.GetTransactions
{
    public sealed record GetTransactionsQuery() : ICachedQuery<List<TransactionModel>>
    {
        public CacheKey CacheKey => CacheKey.Transactions(null);
        public TimeSpan? Expiration => null;
    }
}
