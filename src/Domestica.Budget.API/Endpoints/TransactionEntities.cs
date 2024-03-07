using Carter;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Application.TransactionEntities.GetTransactionEntities;
using Domestica.Budget.Application.TransactionEntities.RemoveTransactionEntity;
using Domestica.Budget.Application.TransactionEntities.UpdateTransactionEntity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class TransactionEntities : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "transaction-entities",
                async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionEntitiesQuery(new(userContext.IdentityId)), cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPost(
                "transaction-entities",
                async (AddTransactionEntityCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPut(
                "transaction-entities/{transactionEntityId}",
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
                }).RequireAuthorization();

            app.MapDelete(
                "transaction-entities/{transactionEntityId}",
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
                }).RequireAuthorization();
        }
    }
}
