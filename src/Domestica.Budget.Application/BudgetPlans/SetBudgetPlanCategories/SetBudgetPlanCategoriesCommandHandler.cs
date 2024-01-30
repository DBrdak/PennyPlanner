using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    internal sealed class SetBudgetPlanCategoriesCommandHandler : ICommandHandler<SetBudgetPlanCategoriesCommand, BudgetPlan>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetBudgetPlanCategoriesCommandHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BudgetPlan>> Handle(SetBudgetPlanCategoriesCommand request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId, cancellationToken);

            if (budgetPlan is null)
            {
                return Result.Failure<BudgetPlan>(Error.NotFound("Budget plan not found"));
            }

            foreach (var budgetedTransactionCategoryValues in request.BudgetedTransactionCategoryValues)
            {
                budgetPlan.SetBudgetForCategory(
                    TransactionCategory.FromValue(budgetedTransactionCategoryValues.Category),
                    budgetedTransactionCategoryValues.BudgetedAmount.ToDomainObject());
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(budgetPlan);
            }

            return Result.Failure<BudgetPlan>(Error.TaskFailed("Problem while setting budgeted categories"));
        }
    }
}
