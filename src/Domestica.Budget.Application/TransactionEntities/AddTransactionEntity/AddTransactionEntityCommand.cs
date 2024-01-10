using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    public sealed record AddTransactionEntityCommand(TransactionEntityName Name, TransactionEntityType Type) : ICommand
    {
    }
}
