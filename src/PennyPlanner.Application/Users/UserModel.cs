using System.Text.Json.Serialization;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.Users
{
    public sealed record UserModel
    {
        public string UserId { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string Currency { get; init; }
        public bool IsEmailVerified { get; init; }

        private UserModel(string userId, string email, string currency, string username, bool isEmailVerified)
        {
            UserId = userId;
            Email = email;
            Currency = currency;
            Username = username;
            IsEmailVerified = isEmailVerified;
        }

        [JsonConstructor]
        private UserModel()
        {
        }

        internal static UserModel FromDomainObject(User user) =>
            new(
                user.Id.Value.ToString(),
                user.Email.Value,
                user.Currency.Code,
                user.Username.Value,
                user.IsEmailVerified
            );
    }
}
