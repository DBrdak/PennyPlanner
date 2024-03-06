using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.Users;
using Responses.DB;

namespace Domestica.Budget.Application.Users.LogInUser
{
    internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessToken>
    {
        private readonly IJwtService _jwtService;

        public LogInUserCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<Result<AccessToken>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _jwtService.GetAccessTokenAsync(
                request.Email,
                request.Password,
                cancellationToken);

            return result.IsFailure 
                ? Result.Failure<AccessToken>(UserErrors.InvalidCredentials) 
                : new AccessToken(result.Value);
        }
    }
}
