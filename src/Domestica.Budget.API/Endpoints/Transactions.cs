using Carter;
using Domestica.Budget.Application.Transactions.AddIncomeTransaction;
using Domestica.Budget.Application.Transactions.AddInternalTransaction;
using Domestica.Budget.Application.Transactions.AddOutcomeTransaction;
using Domestica.Budget.Application.Transactions.GetTransactions;
using Domestica.Budget.Application.Transactions.RemoveTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class Transactions : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "transactions",
                async (ISender sender, IDistributedCache cache, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionsQuery(), cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                });

            app.MapPost(
                "transactions/internal",
                async (AddInternalTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapPost(
                "transactions/income",
                async (AddIncomeTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapPost(
                "transactions/outcome",
                async (AddOutcomeTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapDelete(
                "transactions/{transactionId}",
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
                });
        }
    }
}
