using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Domain.BudgetPlans.DomainEvents;

#pragma warning disable CS8618
public sealed record BudgetPlanCategoryBudgetedDomainEvent(BudgetPlanId CreatedBudgetPlanId, TransactionCategory Category) : IDomainEvent
{
}