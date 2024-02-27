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
    internal sealed class TransactionDeletedDomainEventHandler : IDomainEventHandler<TransactionDeletedDomainEvent>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionDeletedDomainEventHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TransactionDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(
                notification.DeletedTransaction.TransactionDateUtc,
                cancellationToken);

            if (budgetPlan is null)
            {
                return;
            }

            budgetPlan.RemoveTransaction(notification.DeletedTransaction);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!isSuccessful)
            {
                throw new DBConcurrencyException($"Problem while removing transaction with ID: {notification.DeletedTransaction.Id.Value} from budget plan with ID: {budgetPlan.Id.Value}");
            }
        }
    }
}
