using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.Users;

public record UserId : EntityId
{
    public UserId(Guid value) : base(value)
    {

    }

    public UserId(string value) : base(Guid.Parse(value))
    {

    }

    public UserId() : base(Guid.NewGuid())
    {

    }
}