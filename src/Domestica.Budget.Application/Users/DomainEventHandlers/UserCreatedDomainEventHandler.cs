﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Application.Abstractions.Email;
using Domestica.Budget.Domain.Users;
using Domestica.Budget.Domain.Users.Events;

namespace Domestica.Budget.Application.Users.DomainEventHandlers
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

            await _emailService.SendAsync(user.Email, "Verify the email", "");
        }
    }
}
