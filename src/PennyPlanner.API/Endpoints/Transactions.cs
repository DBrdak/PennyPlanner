using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.Transactions.AddIncomeTransaction;
using PennyPlanner.Application.Transactions.AddInternalTransaction;
using PennyPlanner.Application.Transactions.AddOutcomeTransaction;
using PennyPlanner.Application.Transactions.GetTransactions;
using PennyPlanner.Application.Transactions.RemoveTransaction;

namespace PennyPlanner.API.Endpoints
{
    public sealed class Transactions : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/transactions",
                async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionsQuery(new(Guid.Parse(userContext.IdentityId))), cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPost(
                "api/transactions/internal",
                async (AddInternalTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPost(
                "api/transactions/income",
                async (AddIncomeTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPost(
                "api/transactions/outcome",
                async (AddOutcomeTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapDelete(
                "api/transactions/{transactionId}",
                async (
                    [FromRoute] string transactionId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = new RemoveTransactionCommand(transactionId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();
        }
    }
}
