using Carter;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Application.TransactionCategories.DeleteTransactionCategory;
using Domestica.Budget.Application.TransactionCategories.GetTransactionCategories;
using Domestica.Budget.Application.TransactionCategories.UpdateTransactionCategory;
using MediatR;

namespace Domestica.Budget.API.Endpoints
{
    public class TransactionCategories : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/transaction-categories",
                async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionCategoriesQuery(new(userContext.IdentityId)), cancellationToken);

                    return Results.Ok(result.Value);
                }).RequireAuthorization();

            app.MapPost("api/transaction-categories",
                async (ISender sender, AddTransactionCategoryCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPut(
                "api/transaction-categories/{transactionCategoryId}",
                async (
                    ISender sender,
                    UpdateTransactionCategoryCommand command,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapDelete("api/transaction-categories/{transactionCategoryId}",
                async (ISender sender, string transactionCategoryId, CancellationToken cancellationToken) =>
                {
                    var command = new DeleteTransactionCategoryCommand(transactionCategoryId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();
        }
    }
}
