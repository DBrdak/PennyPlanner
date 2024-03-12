using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionCategories;

namespace PennyPlanner.Application.TransactionCategories.UpdateTransactionCategory
{
    public sealed record UpdateTransactionCategoryCommand(string TransactionCategoryId, string NewValue) : ICommand<TransactionCategory>
    {
    }
}
