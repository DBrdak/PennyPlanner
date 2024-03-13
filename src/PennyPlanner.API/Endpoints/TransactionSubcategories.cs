using Carter;
using MediatR;
using PennyPlanner.Application.TransactionSubcategories.AddTransactionSubcategory;
using PennyPlanner.Application.TransactionSubcategories.RemoveTransactionSubcategory;
using PennyPlanner.Application.TransactionSubcategories.UpdateTransactionSubcategory;

namespace PennyPlanner.API.Endpoints
{
    public class TransactionSubcategories : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/transaction-subcategories",
                async (ISender sender, AddTransactionSubcategoryCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");

            app.MapPut(
                "api/transaction-subcategories/{transactionSubcategoryId}",
                async (
                    ISender sender,
                    UpdateTransactionSubcategoryCommand command,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");

            app.MapDelete("api/transaction-subcategories/{transactionSubcategoryId}",
                async (ISender sender, string transactionSubcategoryId, CancellationToken cancellationToken) =>
                {
                    var command = new RemoveTransactionSubcategoryCommand(transactionSubcategoryId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");
        }
    }
}
