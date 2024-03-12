using CommonAbstractions.DB.Entities;
using PennyPlanner.Domain.TransactionCategories;

namespace PennyPlanner.Domain.BudgetPlans.DomainEvents;

#pragma warning disable CS8618
public sealed record BudgetPlanCategoryBudgetedDomainEvent(BudgetPlanId CreatedBudgetPlanId, TransactionCategory Category) : IDomainEvent
{
}