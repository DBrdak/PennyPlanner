using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PennyPlanner.Infrastructure.Authentication.Models;
using Responses.DB;

namespace PennyPlanner.Infrastructure.Authorization.EmailVerifiedRequirement
{
    internal sealed class EmailVerifiedAuthorizationHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AuthorizationErrorWriter _errorWriter;

        public EmailVerifiedAuthorizationHandler(IServiceProvider serviceProvider, AuthorizationErrorWriter errorWriter)
        {
            _serviceProvider = serviceProvider;
            _errorWriter = errorWriter;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            if (context.User.HasClaim(UserRepresentationModel.EmailVerifiedClaimName, true.ToString()))
            {
                context.Succeed(requirement);
                return;
            }

            await _errorWriter.Write(AuthorizationErrors.EmailVerifiedAuthorizationError);
            context.Fail();
        }
    }
}
