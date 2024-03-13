using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.Users.VerifyEmail
{
    internal sealed class VerifyEmailCommandHandler : ICommandHandler<VerifyEmailCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailVerificationService _emailVerificationService;

        public VerifyEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailVerificationService emailVerificationService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _emailVerificationService = emailVerificationService;
        }

        public async Task<Result<User>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new(request.Email), cancellationToken);

            if (user is null)
            {
                return Result.Failure<User>(Error.NotFound($"User with email: {request.Email} not found"));
            }

            var decryptedUserId = _emailVerificationService.DecryptEmailVerificationToken(request.Token);

            if (!IsValid(decryptedUserId, user))
            {
                return Result.Failure<User>(Error.InvalidRequest("Invalid token"));
            }

            user.VerifyEmail();
            var isSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return isSuccess ?
                    Result.Success(user) :
                    Result.Failure<User>(Error.TaskFailed("User email verification failed"));
        }

        private bool IsValid(string decryptedUserId, User user) => decryptedUserId == user.Id.Value.ToString();
    }
}
