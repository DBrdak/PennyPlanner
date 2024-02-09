using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.TransactionCategories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Money.DB;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    internal sealed class SetBudgetPlanCategoriesCommandHandler : ICommandHandler<SetBudgetPlanCategoriesCommand, BudgetPlan>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITransactionCategoryRepository _categoryRepository;

        public SetBudgetPlanCategoriesCommandHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork, IServiceScopeFactory serviceScopeFactory, ITransactionCategoryRepository categoryRepository)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
            _serviceScopeFactory = serviceScopeFactory;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<BudgetPlan>> Handle(SetBudgetPlanCategoriesCommand request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var budgetPlan = await _budgetPlanRepository.GetByIdAsync(request.BudgetPlanId, cancellationToken);

            if (budgetPlan is null)
            {
                return Result.Failure<BudgetPlan>(Error.NotFound("Budget plan not found"));
            }
            
            //TODO Fetch currency from user
            var currency = Currency.Usd;

            foreach (var budgetedTransactionCategoryValues in request.BudgetedTransactionCategoryValues)
            {
                var category = await _categoryRepository.GetByValueAsync<TransactionCategory>(
                    new(budgetedTransactionCategoryValues.CategoryValue),
                    cancellationToken);

                if (category is null)
                {
                    var categoryCreateResult = await CreateCategory(
                        budgetedTransactionCategoryValues.CategoryValue,
                        TransactionCategoryType.FromString(budgetedTransactionCategoryValues.CategoryValue) == TransactionCategoryType.Income ?
                            TransactionCategoryType.Income :
                            TransactionCategoryType.Outcome);

                    if (categoryCreateResult.IsFailure)
                    {
                        return Result.Failure<BudgetPlan>(categoryCreateResult.Error);
                    }

                    category = categoryCreateResult.Value;
                }

                budgetPlan.SetBudgetForCategory(
                    category!,
                    new (budgetedTransactionCategoryValues.BudgetedAmount, currency));
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(budgetPlan);
            }

            return Result.Failure<BudgetPlan>(Error.TaskFailed("Problem while setting budgeted categories"));
        }
        private async Task<Result<TransactionCategory>> CreateCategory(string categoryValue, TransactionCategoryType categoryType)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
            var command = new AddTransactionCategoryCommand(categoryValue, categoryType.Value);
            var categoryCreateResult = await mediator.Send(command);

            return categoryCreateResult;
        }
    }
}
