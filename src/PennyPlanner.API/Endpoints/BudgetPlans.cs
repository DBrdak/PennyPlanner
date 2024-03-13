﻿using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PennyPlanner.Application.BudgetPlans.GetBudgetPlan;
using PennyPlanner.Application.BudgetPlans.SetBudgetPlanCategories;
using PennyPlanner.Application.BudgetPlans.UpdateBudgetPlanCategory;

namespace PennyPlanner.API.Endpoints
{
    public sealed class BudgetPlans : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/budget-plans",
                async (ISender sender, CancellationToken cancellationToken, [FromQuery]DateTime onDate) =>
                {
                    var query = new GetBudgetPlanQuery(onDate);

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.Ok();
                }).RequireAuthorization().RequireRateLimiting("fixed-loose");

            app.MapPost(
                "api/budget-plans",
                async (
                    [FromBody] SetBudgetPlanCategoriesCommand command,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");

            app.MapPut(
                "api/budget-plans/{budgetPlanId}/{budgetedCategoryId}",
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
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");
        }
    }
}
