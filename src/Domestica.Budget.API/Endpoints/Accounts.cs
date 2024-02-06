using Carter;
using Domestica.Budget.Application.Accounts.AddAccount;
using Domestica.Budget.Application.Accounts.GetAccounts;
using Domestica.Budget.Application.Accounts.RemoveAccount;
using Domestica.Budget.Application.Accounts.UpdateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class Accounts : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "accounts",
                async (ISender sender, IDistributedCache cache, CancellationToken cancellationToken) =>
                {
                    var query = new GetAccountsQuery();

                    // TODO key for specific user accounts: "accounts_{userId}"
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

            app.MapPut(
                "accounts/{accountId}",
                async (
                    AccountUpdateData accountUpdateData,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = new UpdateAccountCommand(accountUpdateData);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapDelete(
                "accounts/{accountId}",
                async (
                    ISender sender,
                    [FromRoute] string accountId,
                    CancellationToken cancellationToken) =>
                {
                    var command = new RemoveAccountCommand(accountId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });
        }
    }
}
