using PennyPlanner.Application.Abstractions.Messaging;
using PennyPlanner.Application.Abstractions.Messaging.Caching;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.Transactions.GetTransactions
{
    public sealed record GetTransactionsQuery(UserId UserId) : ICachedQuery<List<TransactionModel>>
    {
        public CacheKey CacheKey => CacheKey.Transactions(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
