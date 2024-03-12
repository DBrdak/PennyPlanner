using CommonAbstractions.DB.Messaging;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.BudgetPlans;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.BudgetPlans.GetBudgetPlan
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
