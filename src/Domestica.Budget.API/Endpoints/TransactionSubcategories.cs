using Carter;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Application.TransactionCategories.UpdateTransactionCategory;
using Domestica.Budget.Application.TransactionSubcategories.AddTransactionSubcategory;
using Domestica.Budget.Application.TransactionSubcategories.RemoveTransactionSubcategory;
using Domestica.Budget.Application.TransactionSubcategories.UpdateTransactionSubcategory;
using MediatR;

namespace Domestica.Budget.API.Endpoints
{
    public class TransactionSubcategories : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("transaction-subcategories",
                async (ISender sender, AddTransactionSubcategoryCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapPut(
                "transaction-subcategories/{transactionSubcategoryId}",
                async (
                    ISender sender,
                    UpdateTransactionSubcategoryCommand command,
                    CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });

            app.MapDelete("transaction-subcategories/{transactionSubcategoryId}",
                async (ISender sender, string transactionSubcategoryId, CancellationToken cancellationToken) =>
                {
                    var command = new RemoveTransactionSubcategoryCommand(transactionSubcategoryId);

                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess ?
                        Results.Ok() :
                        Results.BadRequest(result.Error);
                });
        }
    }
}
