﻿using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    public sealed record SetBudgetPlanCategoriesCommand(
        DateTime BudgetPlanForDate,
        IEnumerable<BudgetedTransactionCategoryValues> BudgetedTransactionCategoryValues) : ICommand<BudgetPlan>
    {
    }
}
