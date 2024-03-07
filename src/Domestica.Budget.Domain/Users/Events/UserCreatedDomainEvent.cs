using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(UserIdentityId UserId) : IDomainEvent
    {
    }
}
