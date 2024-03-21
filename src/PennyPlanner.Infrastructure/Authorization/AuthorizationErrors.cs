using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Responses.DB;

namespace PennyPlanner.Infrastructure.Authorization
{
    internal static class AuthorizationErrors
    {
        public static Error EmailVerifiedAuthorizationError = new(
            "AuthorizationError.EmailVerifiedAuthorizationError",
            "Please verify email");
    }
}
