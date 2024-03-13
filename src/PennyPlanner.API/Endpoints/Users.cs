using Carter;
using MediatR;
using PennyPlanner.Application.Users.GetCurrentUser;
using PennyPlanner.Application.Users.LogInUser;
using PennyPlanner.Application.Users.RegisterUser;
using PennyPlanner.Application.Users.ResendVerificationEmail;
using PennyPlanner.Application.Users.VerifyEmail;

namespace PennyPlanner.API.Endpoints
{
    public sealed class Users : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "api/users/current",
                async (ISender sender, CancellationToken cancellationToken) =>
                {
                    var query = new GetCurrentUserQuery();

                    var result = await sender.Send(query, cancellationToken);

                    return result.IsSuccess 
                        ? Results.Ok(result.Value)
                        : Results.NotFound(result.Error);
                }).RequireAuthorization().RequireRateLimiting("fixed-loose");

            app.MapPost(
                "api/users/login",
                async (ISender sender, LogInUserCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok(result.Value)
                        : Results.BadRequest(result.Error);
                }).RequireRateLimiting("fixed-standard");

            app.MapPost(
                "api/users/register",
                async (ISender sender, RegisterUserCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok()
                        : Results.BadRequest(result.Error);
                }).RequireRateLimiting("fixed-standard");

            app.MapPut(
                "api/users/verify-email",
                async (ISender sender, VerifyEmailCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok()
                        : Results.BadRequest(result.Error);
                }).RequireRateLimiting("fixed-strict");

            app.MapPut(
                "api/users/resend-verification-email",
                async (ISender sender, ResendVerificationEmailCommand command, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(command, cancellationToken);

                    return result.IsSuccess
                        ? Results.Ok()
                        : Results.BadRequest(result.Error);
                }).RequireRateLimiting("fixed-strict");
        }
    }
}
