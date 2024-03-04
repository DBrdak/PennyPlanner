using Carter;
using Domestica.Budget.Application.Users.GetCurrentUser;
using Domestica.Budget.Application.Users.LogInUser;
using Domestica.Budget.Application.Users.RegisterUser;
using MediatR;
using System.Threading;

namespace Domestica.Budget.API.Endpoints
{
    public sealed class Users : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "users/current",
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new GetCurrentUserQuery();

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess 
                        ? Results.Ok(result.Value)
                        : Results.NotFound(result.Error);
                });

            app.MapPost(
                "users/login",
                async (ISender sender, LogInUserCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : Results.Unauthorized();
                });

            app.MapPost(
                "users/register",
                async (ISender sender, RegisterUserCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : Results.BadRequest(result.Error);
                });
        }
    }
}
