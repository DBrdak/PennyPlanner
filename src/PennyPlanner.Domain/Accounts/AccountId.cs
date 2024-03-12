using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.Accounts
{
    public sealed record AccountId : EntityId
    {
        public AccountId(Guid value) : base(value)
        {

        }

        public AccountId() : base(Guid.NewGuid())
        {
            
        }
    }
}
