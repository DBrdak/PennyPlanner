using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    public sealed record GetTransactionCategoriesQuery(UserId UserId) : ICachedQuery<IEnumerable<TransactionCategoryModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionCategories(UserId.Value.ToString());
        public TimeSpan? Expiration => null;
    }
}
