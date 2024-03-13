using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.TransactionEntities.AddTransactionEntity;
using PennyPlanner.Application.TransactionEntities.GetTransactionEntities;
using PennyPlanner.Application.TransactionEntities.RemoveTransactionEntity;
using PennyPlanner.Application.TransactionEntities.UpdateTransactionEntity;

namespace PennyPlanner.API.Endpoints
{
    public sealed class TransactionEntities : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/transaction-entities",
                async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionEntitiesQuery(new(Guid.Parse(userContext.IdentityId))), cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");

            app.MapPost(
                "api/transaction-entities",
                async (AddTransactionEntityCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");

            app.MapPut(
                "api/transaction-entities/{transactionEntityId}",
                async (
                    [FromRoute] string transactionEntityId,
                    UpdateTransactionEntityCommand command,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");

            app.MapDelete(
                "api/transaction-entities/{transactionEntityId}",
                async (
                    ISender sender,
                    [FromRoute] string transactionEntityId,
                    CancellationToken cancellationToken) =>
                {
                    var command = new RemoveTransactionEntityCommand(transactionEntityId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-standard");
        }
    }
}
