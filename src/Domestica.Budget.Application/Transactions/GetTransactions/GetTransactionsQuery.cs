using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Transactions.GetTransactions
{
    public sealed record GetTransactionsQuery(UserId UserId) : ICachedQuery<List<TransactionModel>>
    {
        public CacheKey CacheKey => CacheKey.Transactions(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
