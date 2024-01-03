using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;

namespace Budgetify.Domain.BudgetPlans
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
