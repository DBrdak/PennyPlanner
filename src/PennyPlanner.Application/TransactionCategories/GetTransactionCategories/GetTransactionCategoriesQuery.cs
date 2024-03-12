using PennyPlanner.Application.Abstractions.Messaging;
using PennyPlanner.Application.Abstractions.Messaging.Caching;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.TransactionCategories.GetTransactionCategories
{
    public sealed record GetTransactionCategoriesQuery(UserId UserId) : ICachedQuery<IEnumerable<TransactionCategoryModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionCategories(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
