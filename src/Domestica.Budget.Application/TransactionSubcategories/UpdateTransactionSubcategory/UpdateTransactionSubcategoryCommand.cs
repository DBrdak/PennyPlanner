using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionSubcategories;

namespace Domestica.Budget.Application.TransactionSubcategories.UpdateTransactionSubcategory
{
    public sealed record UpdateTransactionSubcategoryCommand(string TransactionSubcategoryId, string NewValue) : ICommand<TransactionSubcategory>
    {
    }
}
