using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionSubcategories;

namespace PennyPlanner.Application.TransactionSubcategories.RemoveTransactionSubcategory
{
    public sealed record RemoveTransactionSubcategoryCommand(string TransactionSubcategoryId) : ICommand<TransactionSubcategory>
    {
    }
}
