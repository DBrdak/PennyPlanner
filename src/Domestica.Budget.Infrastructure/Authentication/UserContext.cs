﻿using System.Security.Claims;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Infrastructure.Authentication.Models;
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
                .SingleOrDefault(claim => claim.Type == nameof(UserRepresentationModel.Id))?
                .Value ??
            throw new ApplicationException("User context is unavailable");

        public string UserCurrencyCode =>
            _httpContextAccessor
                .HttpContext?
                .User
                .Claims
                .SingleOrDefault(claim => claim.Type == nameof(UserRepresentationModel.Currency))?
                .Value ??
            throw new ApplicationException("User context is unavailable");

        public string? TryGetIdentityId()
        {
            try
            {
                var identityId = IdentityId;
                return identityId;
            }
            catch (ApplicationException)
            {
                return null;
            }
        }
    }
}
