using Carter;
using DateKit.DB;
using Domestica.Budget.Application.BudgetPlans.GetBudgetPlan;
using Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories;
using Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class BudgetPlans : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "budget-plans",
                async (ISender sender, CancellationToken cancellationToken, [FromQuery]DateTime onDate) =>
                {
                    var query = new GetBudgetPlanQuery(onDate);

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.Ok();
                }).RequireAuthorization();

            app.MapPost(
                "budget-plans",
                async (
                    [FromBody] SetBudgetPlanCategoriesCommand command,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPut(
                "budget-plans/{budgetPlanId}/{budgetedCategoryId}",
                async (
                    [FromRoute] string budgetPlanId,
                    [FromRoute] string budgetedCategoryId,
                    [FromBody] UpdateBudgetPlanCategoryValues values,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = new UpdateBudgetPlanCategoryCommand(
                        budgetPlanId,
                        budgetedCategoryId,
                        values);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();
        }
    }
}
