﻿using Carter;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Application.Accounts.AddAccount;
using Domestica.Budget.Application.Accounts.GetAccounts;
using Domestica.Budget.Application.Accounts.RemoveAccount;
using Domestica.Budget.Application.Accounts.UpdateAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class Accounts : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/accounts",
                async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
                {
                    var query = new GetAccountsQuery(new (Guid.Parse(userContext.IdentityId)));

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPost(
                "api/accounts",
                async (NewAccountData newAccountData, ISender sender, CancellationToken cancellationToken) =>
                {
                    var command = new AddAccountCommand(newAccountData);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                }).RequireAuthorization();

            app.MapPut(
                "api/accounts/{accountId}",
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
                }).RequireAuthorization();

            app.MapDelete(
                "api/accounts/{accountId}",
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
                }).RequireAuthorization();
        }
    }
}
