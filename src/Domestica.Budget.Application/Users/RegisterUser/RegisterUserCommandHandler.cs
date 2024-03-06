using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.Users;
using Money.DB;
using Responses.DB;

namespace Domestica.Budget.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserModel>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(
            IAuthenticationService authenticationService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserAsync(request, cancellationToken);

            if (validationResult.IsFailure)
            {
                return (Result<UserModel>)validationResult;
            }

            var user = User.Create(new Email(request.Email), Currency.FromCode(request.Currency));

            var identityId = await _authenticationService.RegisterAsync(
                user,
                request.Password,
                cancellationToken);

            user.SetIdentityId(identityId);

            await _userRepository.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return UserModel.FromDomainObject(user);
        }

        private async Task<Result> ValidateUserAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isEmailUnique = await _userRepository.GetByEmailAsync(new(request.Email), cancellationToken, true) is null;

            if (!isEmailUnique)
            {
                return Result.Failure<UserModel>(Error.InvalidRequest($"Email: {request.Email} is taken"));
            }

            return Result.Success();
        }
    }
}
