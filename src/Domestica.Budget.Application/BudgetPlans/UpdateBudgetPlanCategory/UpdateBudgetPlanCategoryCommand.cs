using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed record UpdateBudgetPlanCategoryCommand : ICommand<BudgetPlan>
    {
        public BudgetPlanId BudgetPlanId { get; init; }
        public TransactionCategory Category { get; init; }
        public Money.DB.Money? NewBudgetedAmount { get; init; }
        public bool IsBudgetToReset { get; init; }

        public UpdateBudgetPlanCategoryCommand(BudgetPlanId BudgetPlanId,
            TransactionCategory Category,
            UpdateBudgetPlanCategoryValues values)
        {
            this.BudgetPlanId = BudgetPlanId;
            this.Category = Category;
            this.NewBudgetedAmount = values.NewBudgetAmount;
            this.IsBudgetToReset = values.IsBudgetToReset;
        }
    }
}
