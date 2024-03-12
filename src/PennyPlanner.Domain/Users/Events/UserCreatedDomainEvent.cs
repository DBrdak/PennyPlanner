using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent
    {
    }
}
