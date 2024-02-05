using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Transactions.DomainEvents
{
    public sealed record TransactionCreatedDomainEvent(Transaction CreatedTransaction) : IDomainEvent
    {
    }
}
