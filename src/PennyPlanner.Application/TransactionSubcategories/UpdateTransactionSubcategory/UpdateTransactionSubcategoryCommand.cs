using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionSubcategories;

namespace PennyPlanner.Application.TransactionSubcategories.UpdateTransactionSubcategory
{
    public sealed record UpdateTransactionSubcategoryCommand(string TransactionSubcategoryId, string NewValue) : ICommand<TransactionSubcategory>
    {
    }
}
