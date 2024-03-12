namespace PennyPlanner.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed record UpdateBudgetPlanCategoryValues(decimal? NewBudgetAmount, bool IsBudgetToReset)
    {
    }
}
