using Carter;
using DateKit.DB;
using Domestica.Budget.Application.BudgetPlans.CreateBudgetPlan;
using Domestica.Budget.Application.BudgetPlans.GetBudgetPlans;
using Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class BudgetPlans : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "budget-plans",
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new GetBudgetPlansQuery();

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                });

            app.MapPost(
                "budget-plans",
                async (DateTimeRange budgetPeriod, ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new CreateBudgetPlanCommand(budgetPeriod);

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapPut(
                "budget-plans/{budgetPlanId}",
                async (
                    [FromBody]IEnumerable<BudgetedTransactionCategoryValues> budgetedTransactionCategoryValues,
                    [FromRoute] string budgetPlanId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var query = new SetBudgetPlanCategoriesCommand(new(Guid.Parse(budgetPlanId)), budgetedTransactionCategoryValues);

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });
        }
    }
}
