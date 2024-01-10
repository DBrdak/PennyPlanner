using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.BudgetPlans
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
