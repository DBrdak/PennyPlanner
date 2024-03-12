using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.Users;
using Responses.DB;

namespace Domestica.Budget.Application.Users.GetCurrentUser
{
    internal sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserModel>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;

        public GetCurrentUserQueryHandler(IUserContext userContext, IUserRepository userRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
        }

        public async Task<Result<UserModel>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                new UserId(_userContext.IdentityId),
                cancellationToken,
                true);

            if (user is null)
            {
                return Result.Failure<UserModel>(
                    Error.NotFound($"Current user cannot be accessed. IdentityId: {_userContext.IdentityId}"));
            }

            return UserModel.FromDomainObject(user);
        }
    }
}
