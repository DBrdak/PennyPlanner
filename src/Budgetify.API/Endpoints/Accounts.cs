using Budgetify.Application.Accounts.AddAccount;
using Budgetify.Application.Accounts.GetAccounts;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Budgetify.API.Endpoints
{
    public sealed class Accounts : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "accounts",
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new GetAccountsQuery();

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                });

            app.MapPost(
                "accounts",
                async (NewAccountData newAccountData, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new AddAccountCommand(newAccountData);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });
        }
    }
}
