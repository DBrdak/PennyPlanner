using System.Data;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions.DomainEvents;

namespace Domestica.Budget.Application.Transactions.DomainEventHandlers
{
    /// <summary>
    /// Handles transaction creation by adding it to the budget plan.
    /// </summary>
    internal sealed class TransactionCreatedDomainEventHandler : IDomainEventHandler<TransactionCreatedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetPlanRepository _budgetPlanRepository;

        public TransactionCreatedDomainEventHandler(IUnitOfWork unitOfWork, IBudgetPlanRepository budgetPlanRepository)
        {
            _unitOfWork = unitOfWork;
            _budgetPlanRepository = budgetPlanRepository;
        }

        public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(notification.CreatedTransaction.TransactionDateUtc, cancellationToken);

            if (budgetPlan is null)
            {
                return;
            }

            budgetPlan.AddTransaction(notification.CreatedTransaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
