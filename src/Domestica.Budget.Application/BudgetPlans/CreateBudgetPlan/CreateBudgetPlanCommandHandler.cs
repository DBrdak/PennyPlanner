using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;
using Responses.DB;

namespace Domestica.Budget.Application.BudgetPlans.CreateBudgetPlan
{
    internal sealed class CreateBudgetPlanCommandHandler : ICommandHandler<CreateBudgetPlanCommand, BudgetPlan>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBudgetPlanCommandHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BudgetPlan>> Handle(CreateBudgetPlanCommand request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id

            var budgetPlan = new BudgetPlan(request.BudgetPeriod);

            await _budgetPlanRepository.AddAsync(budgetPlan, cancellationToken);
            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(budgetPlan);
            }

            return Result.Failure<BudgetPlan>(Error.TaskFailed("Problem while adding new budget plan"));
        }
    }
}
