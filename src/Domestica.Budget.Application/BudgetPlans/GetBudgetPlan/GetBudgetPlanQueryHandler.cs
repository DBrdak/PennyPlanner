using CommonAbstractions.DB.Messaging;
using DateKit.DB;
using Domestica.Budget.Domain.BudgetPlans;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlan
{
    internal sealed class
        GetBudgetPlanQueryHandler : IQueryHandler<GetBudgetPlanQuery, BudgetPlanModel>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;

        public GetBudgetPlanQueryHandler(IBudgetPlanRepository budgetPlanRepository)
        {
            _budgetPlanRepository = budgetPlanRepository;
        }


        public async Task<Result<BudgetPlanModel>> Handle(GetBudgetPlanQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(request.ValidOnDate, cancellationToken, true);

            return budgetPlan is null ?
                BudgetPlanModel.FromDomainObject(BudgetPlan.CreateForMonth(request.ValidOnDate)) :
                BudgetPlanModel.FromDomainObject(budgetPlan);
        }

    }
}
