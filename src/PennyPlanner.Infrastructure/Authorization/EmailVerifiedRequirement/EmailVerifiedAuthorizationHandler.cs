using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.Users;
using PennyPlanner.Infrastructure.Authentication.Models;
using Responses.DB;

namespace PennyPlanner.Infrastructure.Authorization.EmailVerifiedRequirement
{
    internal sealed class EmailVerifiedAuthorizationHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AuthorizationErrorWriter _errorWriter;
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;

        public EmailVerifiedAuthorizationHandler(IServiceProvider serviceProvider, AuthorizationErrorWriter errorWriter, IUserRepository userRepository, IUserContext userContext)
        {
            _serviceProvider = serviceProvider;
            _errorWriter = errorWriter;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            if (context.User.HasClaim(UserRepresentationModel.EmailVerifiedClaimName, true.ToString()) ||
                await IsEmailVerified())
            {
                context.Succeed(requirement);
                return;
            }

            await _errorWriter.Write(AuthorizationErrors.EmailVerifiedAuthorizationError);
            context.Fail();
        }

        private async Task<bool> IsEmailVerified()
        {
            var userId = _userContext.TryGetIdentityId();

            if (userId is null)
            {
                return false;
            }

            var user = await _userRepository.GetByIdAsync(new(userId));

            return user?.IsEmailVerified ?? false;
        }
    }
}
