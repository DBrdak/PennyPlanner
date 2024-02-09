using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.TransactionCategories.DeleteTransactionCategory
{
    public sealed record DeleteTransactionCategoryCommand(string TransactionCategoryId) : ICommand<TransactionCategory>
    {
    }
}
