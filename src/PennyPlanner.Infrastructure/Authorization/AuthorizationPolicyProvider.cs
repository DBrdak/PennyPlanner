using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable once ConvertClosureToMethodGroup
namespace PennyPlanner.Infrastructure.Authorization
{
    internal sealed class AuthorizationPolicyProvider
    {
        public static Action<AuthorizationOptions> Configure =>
            configuration =>
            {
                AddEmailVerifiedPolicy(configuration);
            };

        private static void AddEmailVerifiedPolicy(AuthorizationOptions configuration)
        {
            configuration.AddPolicy(
                EmailVerifiedRequirement.EmailVerifiedRequirement.PolicyName,
                builder => builder
                    .AddRequirements(new EmailVerifiedRequirement.EmailVerifiedRequirement())
                    .Build());
        }
    }
}
