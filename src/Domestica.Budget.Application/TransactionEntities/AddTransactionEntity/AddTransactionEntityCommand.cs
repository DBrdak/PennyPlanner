using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    public sealed record AddTransactionEntityCommand(
        string Name,
        string Type) : ICommand<TransactionEntity>
    {
    }
}
