using CommonAbstractions.DB.Entities;
using PennyPlanner.Application.Abstractions.Email;
using PennyPlanner.Domain.Users;
using PennyPlanner.Domain.Users.Events;

namespace PennyPlanner.Application.Users.DomainEventHandlers
{
    internal sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public UserCreatedDomainEventHandler(IEmailService emailService, IUserRepository userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken) ??
                       throw new ApplicationException(
                           $"User with ID: {notification.UserId.Value} not exist, verification email cannot be send");

            var result = await _emailService.SendAsync(user.Email, "Verify the email", "", "");

            if (result.IsFailure)
            {
                throw new ApplicationException(result.Error.Name);
            }
        }
    }
}
