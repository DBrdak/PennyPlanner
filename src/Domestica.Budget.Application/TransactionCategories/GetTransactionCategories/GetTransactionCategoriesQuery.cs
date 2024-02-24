using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Messaging;
using Domestica.Budget.Application.Messaging.Caching;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    public sealed record GetTransactionCategoriesQuery() : ICachedQuery<IEnumerable<TransactionCategoryModel>>
    {
        public CacheKey CacheKey => CacheKey.TransactionCategories(null);
        public TimeSpan? Expiration => null;
    }
}
