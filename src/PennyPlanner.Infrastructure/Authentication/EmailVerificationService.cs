using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PennyPlanner.Infrastructure.Authentication
{
    public sealed class EmailVerificationService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public EmailVerificationService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string GenerateEmailVerificationToken(string userId)
        {
            var protector = _dataProtectionProvider.CreateProtector("EmailVerificationTokenPurpose");
            return protector.Protect(userId);
        }

        public string DecryptEmailVerificationToken(string token)
        {
            var protector = _dataProtectionProvider.CreateProtector("EmailVerificationTokenPurpose");
            return protector.Unprotect(token);
        }
    }
}
