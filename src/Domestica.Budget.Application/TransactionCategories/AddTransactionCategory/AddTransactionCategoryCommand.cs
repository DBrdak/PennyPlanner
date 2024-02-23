using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.TransactionCategories.AddTransactionCategory
{
    public sealed record AddTransactionCategoryCommand(string Value, string Type) : ICommand<TransactionCategory>
    {
    }
}
