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

            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(request.BudgetPlanForDate, cancellationToken);

            if (budgetPlan is null)
            {
                budgetPlan = BudgetPlan.CreateForMonth(request.BudgetPlanForDate);
                await _budgetPlanRepository.AddAsync(budgetPlan, cancellationToken);
            }
            
            //TODO Fetch currency from user
            var currency = Currency.Usd;

            foreach (var budgetedTransactionCategoryValues in request.BudgetedTransactionCategoryValues)
            {
                var category = await GetOrCreateCategory(
                    new (budgetedTransactionCategoryValues.CategoryValue), 
                    TransactionCategoryType.FromString(budgetedTransactionCategoryValues.CategoryType), 
                    cancellationToken);

                budgetPlan.SetBudgetForCategory(
                    category!,
                    new (budgetedTransactionCategoryValues.BudgetedAmount, currency));
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return isSuccessful 
                ? Result.Success(budgetPlan) 
                : Result.Failure<BudgetPlan>(Error.TaskFailed("Problem while setting budgeted categories"));
        }

        private async Task<TransactionCategory?> GetOrCreateCategory(TransactionCategoryValue categoryValue, TransactionCategoryType categoryType, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByValueAsync<TransactionCategory>(
                categoryValue,
                cancellationToken) ??
                           CreateCategory(categoryValue, categoryType);
        }

        private TransactionCategory CreateCategory(TransactionCategoryValue value, TransactionCategoryType type)
        {
            if (type == TransactionCategoryType.Income)
            {
                return new IncomeTransactionCategory(value);
            }

            if (type == TransactionCategoryType.Outcome)
            {
                return new OutcomeTransactionCategory(value);
            }

            return null;
        }
    }
}
