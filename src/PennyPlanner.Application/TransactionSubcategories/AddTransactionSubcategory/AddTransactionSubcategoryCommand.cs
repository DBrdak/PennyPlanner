using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionSubcategories;

namespace PennyPlanner.Application.TransactionSubcategories.AddTransactionSubcategory
{
    public sealed record AddTransactionSubcategoryCommand(string Value, string CategoryId) : ICommand<TransactionSubcategory>
    {
    }
}
