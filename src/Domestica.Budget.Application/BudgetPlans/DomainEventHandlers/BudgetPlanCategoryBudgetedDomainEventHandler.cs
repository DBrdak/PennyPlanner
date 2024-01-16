using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.BudgetPlans.DomainEvents;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.BudgetPlans.DomainEventHandlers
{
    public sealed class BudgetPlanCategoryBudgetedDomainEventHandler : IDomainEventHandler<BudgetPlanCategoryBudgetedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly ITransactionRepository _transactionRepository;

        public BudgetPlanCategoryBudgetedDomainEventHandler(IUnitOfWork unitOfWork, IBudgetPlanRepository budgetPlanRepository, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _budgetPlanRepository = budgetPlanRepository;
            _transactionRepository = transactionRepository;
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
                    notification.Category)).ToList();

            transactions.ForEach(transaction => budget.AddTransaction(transaction));

            var isSuccessfull = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!isSuccessfull)
            {
                throw new DBConcurrencyException(
                    $"Problem while adding existing transactions to budget plan with ID: {budget.Id}");
            }
        }
    }
}
