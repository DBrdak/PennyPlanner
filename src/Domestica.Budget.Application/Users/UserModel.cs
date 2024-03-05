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

        private UserModel(string userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        [JsonConstructor]
        private UserModel()
        { }

        internal static UserModel FromDomainObject(User user) =>
            new(
                user.Id.Value.ToString(),
                user.Email.Value
            );
    }
}
