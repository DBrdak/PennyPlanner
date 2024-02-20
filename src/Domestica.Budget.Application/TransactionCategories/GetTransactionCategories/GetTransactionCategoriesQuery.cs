using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Caching;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Application.Messaging;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    public sealed record GetTransactionCategoriesQuery() : ICachedQuery<IEnumerable<TransactionCategoryDto>>
    {
        public CacheKey CacheKey => CacheKey.TransactionCategories(null);
        public TimeSpan? Expiration => null;
    }
}
