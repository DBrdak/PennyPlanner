using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Money.DB;
using PennyPlanner.Application.Abstractions.Email;
using PennyPlanner.Domain.Users;
using Responses.DB;
using IPasswordService = PennyPlanner.Application.Abstractions.Authentication.IPasswordService;

namespace PennyPlanner.Application.Users.RegisterUser
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordService passwordService,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _emailService = emailService;
        }

        public async Task<Result<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateUserAsync(request, cancellationToken);

            if (validationResult.IsFailure)
            {
                return (Result<User>)validationResult;
            }

            var passwordHash = _passwordService.HashPassword(request.Password);

            var user = User.Create(
                new (request.Username),
                new (request.Email),
                Currency.FromCode(request.Currency),
                passwordHash);

            await _userRepository.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }

        private async Task<Result> ValidateUserAsync(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isEmailUnique = await _userRepository.GetByEmailAsync(new(request.Email), cancellationToken, true) is null;

            if (!isEmailUnique)
            {
                return Result.Failure<User>(Error.InvalidRequest($"Email: {request.Email} is taken"));
            }

            return Result.Success();
        }
    }
}
