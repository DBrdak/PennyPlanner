using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.Transactions.DomainEvents
{
    public sealed record TransactionCreatedDomainEvent(Transaction CreatedTransaction) : IDomainEvent
    {
    }
}
