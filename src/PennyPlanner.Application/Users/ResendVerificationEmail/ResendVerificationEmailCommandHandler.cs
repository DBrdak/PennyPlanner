using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using PennyPlanner.Application.Abstractions.Email;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.Users.ResendVerificationEmail
{
    internal sealed class ResendVerificationEmailCommandHandler : ICommandHandler<ResendVerificationEmailCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public ResendVerificationEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<User>> Handle(ResendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(new(request.Email), cancellationToken);

            if (user is null)
            {
                return Result.Failure<User>(Error.NotFound($"User with email: {request.Email} not found"));
            }

            if (user.IsEmailVerified)
            {
                return Result.Failure<User>(Error.NotFound($"Email: {request.Email} is already verified"));
            }

            var sendResult = await _emailService.SendWelcomeEmailAsync(user.Email, user.Username, user.Id);

            return sendResult.IsSuccess ?
                    Result.Success(user) :
                    Result.Failure<User>(sendResult.Error);
        }
    }
}
