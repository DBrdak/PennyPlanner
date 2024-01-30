using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed record UpdateBudgetPlanCategoryCommand(
        string BudgetPlanId,
        string Category,
        UpdateBudgetPlanCategoryValues Values) : ICommand<BudgetPlan>
    {
    }
}
