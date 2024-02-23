using CommonAbstractions.DB.Messaging;
using DateKit.DB;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.BudgetPlans;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlan
{
    internal sealed class
        GetBudgetPlanQueryHandler : IQueryHandler<GetBudgetPlanQuery, BudgetPlanDto>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;

        public GetBudgetPlanQueryHandler(IBudgetPlanRepository budgetPlanRepository)
        {
            _budgetPlanRepository = budgetPlanRepository;
        }


        public async Task<Result<BudgetPlanDto>> Handle(GetBudgetPlanQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(request.ValidOnDate, cancellationToken, true);

            return budgetPlan is null ?
                BudgetPlanDto.FromDomainObject(BudgetPlan.CreateForMonth(request.ValidOnDate)) :
                BudgetPlanDto.FromDomainObject(budgetPlan);
        }

    }
}
