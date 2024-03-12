using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Users;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlan
{
    internal sealed class
        GetBudgetPlanQueryHandler : IQueryHandler<GetBudgetPlanQuery, BudgetPlanModel>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUserContext _userContext;

        public GetBudgetPlanQueryHandler(IBudgetPlanRepository budgetPlanRepository, IUserContext userContext)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _userContext = userContext;
        }


        public async Task<Result<BudgetPlanModel>> Handle(GetBudgetPlanQuery request, CancellationToken cancellationToken)
        {
            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(request.ValidOnDate, cancellationToken, true);

            return budgetPlan is null ?
                BudgetPlanModel.FromDomainObject(BudgetPlan.CreateForMonth(request.ValidOnDate, new UserId(_userContext.IdentityId))) :
                BudgetPlanModel.FromDomainObject(budgetPlan);
        }

    }
}
