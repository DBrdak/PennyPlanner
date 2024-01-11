﻿using Carter;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Application.TransactionEntities.GetTransactionEntities;
using Domestica.Budget.Application.TransactionEntities.UpdateTransactionEntity;
using Domestica.Budget.Domain.TransactionEntities;
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
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(new GetTransactionEntitiesQuery(), cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok(result.Value) :
                        Results.BadRequest(result.Error);
                });

            app.MapPost(
                "transaction-entities",
                async (AddTransactionEntityCommand command, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapPut(
                "transaction-entities/{transactionEntityId}",
                async (
                    [FromRoute] string transactionEntityId,
                    TransactionEntityName newName,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    var command = new UpdateTransactionEntityCommand(new (Guid.Parse(transactionEntityId)), newName);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });
        }
    }
}
