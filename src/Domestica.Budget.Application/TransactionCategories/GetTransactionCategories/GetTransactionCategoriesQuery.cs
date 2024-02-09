using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    public sealed record GetTransactionCategoriesQuery() : IQuery<IEnumerable<TransactionCategoryDto>>
    {
    }
}
