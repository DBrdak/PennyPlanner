using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Domestica.Budget.Infrastructure.Authentication
{
    public sealed class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string IdentityId =>
            _httpContextAccessor
                .HttpContext?
                .User
                .Claims
                .SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?
                .Value ??
            throw new ApplicationException("User context is unavailable");
    }
}
