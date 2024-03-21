using Carter;
using MediatR;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.TransactionCategories.AddTransactionCategory;
using PennyPlanner.Application.TransactionCategories.DeleteTransactionCategory;
using PennyPlanner.Application.TransactionCategories.GetTransactionCategories;
using PennyPlanner.Application.TransactionCategories.UpdateTransactionCategory;
using PennyPlanner.Infrastructure.Authorization.EmailVerifiedRequirement;

namespace PennyPlanner.API.Endpoints
{
    public class TransactionCategories : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/transaction-categories",
                async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionCategoriesQuery(new(Guid.Parse(userContext.IdentityId))), cancellationToken);

                    return Results.Ok(result.Value);
                }).RequireAuthorization(EmailVerifiedRequirement.PolicyName).RequireRateLimiting("fixed-loose");

            app.MapPost("api/transaction-categories",
                async (ISender sender, AddTransactionCategoryCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization(EmailVerifiedRequirement.PolicyName).RequireRateLimiting("fixed-standard");

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
                }).RequireAuthorization(EmailVerifiedRequirement.PolicyName).RequireRateLimiting("fixed-standard");

            app.MapDelete("api/transaction-categories/{transactionCategoryId}",
                async (ISender sender, string transactionCategoryId, CancellationToken cancellationToken) =>
                {
                    var command = new DeleteTransactionCategoryCommand(transactionCategoryId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization(EmailVerifiedRequirement.PolicyName).RequireRateLimiting("fixed-standard");
        }
    }
}
