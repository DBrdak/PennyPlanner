using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.BudgetPlans
{
    public sealed record BudgetPlanId : EntityId
    {
        public BudgetPlanId(Guid value) : base(value)
        {
            
        }

        public BudgetPlanId(): base(Guid.NewGuid())
        { }
    }
}
