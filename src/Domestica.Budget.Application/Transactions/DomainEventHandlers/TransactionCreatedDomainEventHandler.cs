using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var isSuccessfull = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!isSuccessfull)
            {
                throw new DBConcurrencyException(
                    $"Problem while adding transaction with ID: {notification.CreatedTransaction.Id.Value} to budget plan with ID: {budgetPlan.Id.Value}");
            }
        }
    }
}
