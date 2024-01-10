using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Accounts
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
