using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    public sealed record AddIncomeTransactionCommand(
        string DestinationAccountId,
        string SenderName,
        decimal TransactionAmount,
        string CategoryValue,
        string SubcategoryValue,
        DateTime TransactionDateTime) : ICommand<Transaction>
    {
    }
}
