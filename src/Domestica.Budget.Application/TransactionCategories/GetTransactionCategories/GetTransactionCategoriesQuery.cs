using Domestica.Budget.Application.Abstractions.Messaging;
using Domestica.Budget.Application.Abstractions.Messaging.Caching;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    public sealed record GetTransactionCategoriesQuery(UserIdentityId UserId) : ICachedQuery<IEnumerable<TransactionCategoryModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionCategories(UserId.Value);
        public TimeSpan? Expiration => null;
    }
}
