using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.BudgetPlans;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlans
{
    internal sealed class
        GetBudgetPlansQueryHandler : IQueryHandler<GetBudgetPlansQuery, IReadOnlyCollection<BudgetPlanDto>>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;

        public GetBudgetPlansQueryHandler(IBudgetPlanRepository budgetPlanRepository)
        {
            _budgetPlanRepository = budgetPlanRepository;
        }


        public async Task<Result<IReadOnlyCollection<BudgetPlanDto>>> Handle(GetBudgetPlansQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var budgetPlans = await _budgetPlanRepository.BrowseUserBudgetPlansAsync(cancellationToken);

            return budgetPlans.Select(BudgetPlanDto.FromDomainObject).ToList();
        }
    }
}
