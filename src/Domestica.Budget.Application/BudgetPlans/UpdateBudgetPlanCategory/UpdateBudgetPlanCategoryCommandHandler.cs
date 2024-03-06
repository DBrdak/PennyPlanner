using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.BudgetPlans;
using Money.DB;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    internal sealed class UpdateBudgetPlanCategoryCommandHandler : ICommandHandler<UpdateBudgetPlanCategoryCommand, BudgetPlan>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateBudgetPlanCategoryCommandHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<BudgetPlan>> Handle(UpdateBudgetPlanCategoryCommand request, CancellationToken cancellationToken)
        {
            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByIdAsync(
                new(Guid.Parse(request.BudgetPlanId)),
                cancellationToken);

            if (budgetPlan is null)
            {
                return Result.Failure<BudgetPlan>(Error.NotFound($"Budget plan with ID: {request.BudgetPlanId} not found"));
            }

            var category =
                budgetPlan.BudgetedTransactionCategories.FirstOrDefault(
                    c => c.CategoryId.Value.ToString() == request.CategoryId)?.Category;

            if (category is null)
            {
                return Result.Failure<BudgetPlan>(Error.NotFound($"Budget plan category with ID: {request.CategoryId} not found"));
            }

            if (request.Values.NewBudgetAmount is not null && !request.Values.IsBudgetToReset)
            {
                var currency = Currency.FromCode(_userContext.UserCurrencyCode);
                budgetPlan.UpdateBudgetCategory(category, new((decimal)request.Values.NewBudgetAmount, currency));
            }
            else
            {
                budgetPlan.ResetBudgetCategory(category);
            }

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return isSuccessful 
                ? Result.Success(budgetPlan) 
                : Result.Failure<BudgetPlan>(Error.TaskFailed($"Problem while updating budget plan with ID: {budgetPlan.Id}"));
        }
    }
}
