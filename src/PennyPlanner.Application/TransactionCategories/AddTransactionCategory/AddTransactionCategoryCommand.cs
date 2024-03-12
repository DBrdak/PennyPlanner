using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionCategories;

namespace PennyPlanner.Application.TransactionCategories.AddTransactionCategory
{
    public sealed record AddTransactionCategoryCommand(string Value, string Type) : ICommand<TransactionCategory>
    {
    }
}
