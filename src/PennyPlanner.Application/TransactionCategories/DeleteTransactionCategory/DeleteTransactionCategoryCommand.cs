using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionCategories;

namespace PennyPlanner.Application.TransactionCategories.DeleteTransactionCategory
{
    public sealed record DeleteTransactionCategoryCommand(string TransactionCategoryId) : ICommand<TransactionCategory>
    {
    }
}
