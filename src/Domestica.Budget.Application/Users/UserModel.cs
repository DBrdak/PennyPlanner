using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Users
{
    public sealed record UserModel
    {
        public string UserId { get; init; }
        public string Email { get; init; }
        public string Currency { get; init; }

        private UserModel(string userId, string email, string currency)
        {
            UserId = userId;
            Email = email;
            Currency = currency;
        }

        [JsonConstructor]
        private UserModel()
        { }

        internal static UserModel FromDomainObject(User user) =>
            new(
                user.IdentityId.Value,
                user.Email.Value,
                user.Currency.Code
            );
    }
}
