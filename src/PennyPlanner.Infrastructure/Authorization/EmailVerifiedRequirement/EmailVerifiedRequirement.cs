using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PennyPlanner.Infrastructure.Authorization.EmailVerifiedRequirement
{
    public sealed class EmailVerifiedRequirement : IAuthorizationRequirement
    {
        public static readonly string PolicyName = "EmailVerified";
    }
}
