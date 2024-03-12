using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.Users;
using Responses.DB;

namespace Domestica.Budget.Application.Users.LogInUser
{
    internal sealed class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessToken>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public LogInUserCommandHandler(IJwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<Result<AccessToken>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new(request.Email));

            if (user is null)
            {
                return Result.Failure<AccessToken>(Error.InvalidRequest($"User with email: {request.Email} does not exist"));
            }

            var result = await _jwtService.GetAccessTokenAsync(
                user,
                request.Password,
                cancellationToken);

            return result.IsFailure 
                ? Result.Failure<AccessToken>(UserErrors.InvalidCredentials) 
                : new AccessToken(result.Value);
        }
    }
}
