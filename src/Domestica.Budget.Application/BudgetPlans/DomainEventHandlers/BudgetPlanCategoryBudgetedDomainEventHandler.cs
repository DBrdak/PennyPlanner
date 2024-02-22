using System.Data;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.BudgetPlans.DomainEvents;
using Domestica.Budget.Domain.Transactions;
using Microsoft.Extensions.Logging;

namespace Domestica.Budget.Application.BudgetPlans.DomainEventHandlers
{
    public sealed class BudgetPlanCategoryBudgetedDomainEventHandler : IDomainEventHandler<BudgetPlanCategoryBudgetedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<BudgetPlanCategoryBudgetedDomainEventHandler> _logger;

        public BudgetPlanCategoryBudgetedDomainEventHandler(IUnitOfWork unitOfWork, IBudgetPlanRepository budgetPlanRepository, ITransactionRepository transactionRepository, ILogger<BudgetPlanCategoryBudgetedDomainEventHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _budgetPlanRepository = budgetPlanRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task Handle(BudgetPlanCategoryBudgetedDomainEvent notification, CancellationToken cancellationToken)
        {
            var budget = await _budgetPlanRepository.GetByIdIncludeAsync(
                notification.CreatedBudgetPlanId,
                budgetPlan => budgetPlan.Transactions,
                cancellationToken);

            if (budget is null)
            {
                return;
            }

            var transactions = (
                await _transactionRepository.GetTransactionsByDateAndCategoryAsync(
                    budget.BudgetPeriod,
                    notification.Category,
                    cancellationToken));

            transactions.ForEach(budget.AddTransaction);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogCritical("Error occured during adding existing transactions: {transactions} for budget with ID: {budgetPlanId}. Exception: {exception}", transactions, budget.Id.Value, e);
            }
        }
    }
}
