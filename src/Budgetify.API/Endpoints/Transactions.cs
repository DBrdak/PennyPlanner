using Budgetify.Application.Transactions.AddInternalTransaction;
using Budgetify.Domain.Accounts;
using Carter;
using MediatR;

namespace Budgetify.API.Endpoints
{
    public sealed class Transactions : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut(
                "transactions/internal",
                async (AddInternalTransactionCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });
        }
    }
}
