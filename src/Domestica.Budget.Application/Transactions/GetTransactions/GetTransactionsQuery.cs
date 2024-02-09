using Domestica.Budget.Application.Caching;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Application.Messaging;

namespace Domestica.Budget.Application.Transactions.GetTransactions
{
    public sealed record GetTransactionsQuery() : ICachedQuery<List<TransactionDto>>
    {
        public CacheKey CacheKey => CacheKey.Transactions(null);
        public TimeSpan? Expiration => null;
    }
}
