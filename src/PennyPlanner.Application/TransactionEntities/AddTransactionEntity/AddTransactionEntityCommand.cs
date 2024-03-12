using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionEntities;

namespace PennyPlanner.Application.TransactionEntities.AddTransactionEntity
{
    public sealed record AddTransactionEntityCommand(
        string Name,
        string Type) : ICommand<TransactionEntity>
    {
    }
}
